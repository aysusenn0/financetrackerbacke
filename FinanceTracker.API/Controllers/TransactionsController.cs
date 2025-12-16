// FinanceTracker.API/Controllers/TransactionsController.cs
using FinanceTracker.Application.Features.Transactions; // Komutumuz için
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var newId = await _mediator.Send(command);
            return Ok(new { Id = newId }); 
        }
        // GET /api/transactions
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var query = new GetTransactionsQuery();
            var result = await _mediator.Send(query);
            return Ok(result); 
        }
    }
}