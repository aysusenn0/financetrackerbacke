using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using FinanceTracker.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace FinanceTracker.Application.Features.Currencies
{
    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, List<CurrencyDto>>
    {
        private readonly FinanceTrackerDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

       
        private const string CacheKey = "all_currencies";

        public GetCurrenciesQueryHandler(FinanceTrackerDbContext context, IDistributedCache cache, IMapper mapper)
        {
            _context = context;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<List<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
           
            string cachedCurrencies = await _cache.GetStringAsync(CacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedCurrencies))
            {
                // Varsa: Cache'den döndür
                return JsonSerializer.Deserialize<List<CurrencyDto>>(cachedCurrencies);
            }

            // 2. Yoksa: Veritabanından (DB) çek 
            var currenciesFromDb = await _context.Currencies.AsNoTracking().ToListAsync(cancellationToken);

            // 3. AutoMapper ile DTO'ya dönüştür
            var currencyDtos = _mapper.Map<List<CurrencyDto>>(currenciesFromDb);

            // 4. Cache'i yenile  ve döndür
            await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(currencyDtos), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4) // 4 saat geçerli olsun
            }, cancellationToken);

            return currencyDtos;
        }
    }
}