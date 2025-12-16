using System.Net.Http;          // IHttpClientFactory için
using System.Globalization;     // CultureInfo için
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceTracker.Infrastructure.Persistence;
using FinanceTracker.Infrastructure.Persistence.entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Xml.Linq;
using FinanceTracker.Infrastructure.persistence.entities;




namespace FinanceTracker.Application.Features.Currencies
{
    public class UpdateCurrencyRatesCommandHandler : IRequestHandler<UpdateCurrencyRatesCommand, Unit>
    {
        private readonly FinanceTrackerDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor ile ihtiyacımız olan servisleri (dependency injection) alıyoruz
        public UpdateCurrencyRatesCommandHandler(
            FinanceTrackerDbContext context,
            IDistributedCache cache,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _cache = cache;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Unit> Handle(UpdateCurrencyRatesCommand request, CancellationToken cancellationToken)
        {
           // 1. appsettings.json'dan TCMB'nin XML adresini al 
            var tcmbUrl = _configuration["TCMB:XmlUrl"];
            if (string.IsNullOrEmpty(tcmbUrl))
            {
                throw new Exception("TCMB XML Url'i appsettings.json'da bulunamadı.");
            }

            // 2. HttpClient ile TCMB'den XML'i çek
            var client = _httpClientFactory.CreateClient();
            var xmlStream = await client.GetStreamAsync(tcmbUrl);
            var xmlDoc = XDocument.Load(xmlStream);

            // 3. XML'i parse et (çözümle)
            var currenciesFromXml = xmlDoc.Descendants("Currency")
                .Select(c => new
                {
                    Code = c.Attribute("CurrencyCode")?.Value,
                    Rate = c.Element("ForexSelling")?.Value // Efektif Satış Kuru
                })
                .Where(c => c.Code == "USD" || c.Code == "EUR" || c.Code == "GBP") // Şimdilik 3 ana kuru alalım
                .ToList();

            foreach (var xmlCurrency in currenciesFromXml)
            {
                if (string.IsNullOrEmpty(xmlCurrency.Rate) || string.IsNullOrEmpty(xmlCurrency.Code))
                    continue;

                // 4. Veritabanında bu kur var mı diye bak
                var dbCurrency = await _context.Currencies
                    .FirstOrDefaultAsync(c => c.Code == xmlCurrency.Code, cancellationToken);

                var rateDecimal = Convert.ToDecimal(xmlCurrency.Rate, new System.Globalization.CultureInfo("en-US"));

                if (dbCurrency != null)
                {
                    // Varsa: Güncelle
                    dbCurrency.RateToTRY = rateDecimal;
                    dbCurrency.LastUpdated = DateTime.UtcNow;
                }
                else
                {
                    // Yoksa: Yeni oluştur
                    dbCurrency = new Currency
                    {
                        Code = xmlCurrency.Code,
                        Name = xmlCurrency.Code, // (İsim için ayrı bir eşleme listesi gerekir, şimdilik kodu kullanalım)
                        RateToTRY = rateDecimal,
                        LastUpdated = DateTime.UtcNow
                    };
                    await _context.Currencies.AddAsync(dbCurrency, cancellationToken);
                }
            }

            
            var tryCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.Code == "TRY");
            if (tryCurrency == null)
            {
                await _context.Currencies.AddAsync(new Currency { Code = "TRY", Name = "Türk Lirası", RateToTRY = 1, LastUpdated = DateTime.UtcNow });
            }

           
            await _context.SaveChangesAsync(cancellationToken);

            
            var allCurrencies = await _context.Currencies.AsNoTracking().ToListAsync(cancellationToken);
            var cacheKey = "all_currencies"; // Android'in de bu anahtarı bilmesi gerekecek

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(allCurrencies), new DistributedCacheEntryOptions
            {
                
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4)
            }, cancellationToken);

            return Unit.Value;
        }
    }
}
