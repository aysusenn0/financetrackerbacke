using MediatR;

namespace FinanceTracker.Application.Features.Transactions
{
    
    public class CreateTransactionCommand : IRequest<int> 
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public int TransactionTypeId { get; set; }
    }
}