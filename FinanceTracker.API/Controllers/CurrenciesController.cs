using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Application.Features.Currencies; // Komutumuz için
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMediator _mediator;

       
        public CurrenciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

      
        // POST /api/currencies/update-rates
        [HttpPost("update-rates")]
        public async Task<IActionResult> UpdateRates()
        {
           
            var command = new UpdateCurrencyRatesCommand();
            await _mediator.Send(command);

            return Ok("Kur güncellemesi başarıyla tetiklendi.");
        }
        // GET /api/currencies
        [HttpGet]
        public async Task<IActionResult> GetCurrencies()
        {
            var query = new GetCurrenciesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}