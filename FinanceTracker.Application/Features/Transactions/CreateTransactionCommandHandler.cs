using FinanceTracker.Infrastructure.persistence.entities;
using FinanceTracker.Infrastructure.persistence.entities.FinanceTracker.Infrastructure.Persistence.Entities;
using FinanceTracker.Infrastructure.Persistence;
using FinanceTracker.Infrastructure.Persistence.entities;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly FinanceTrackerDbContext _context;

        public CreateTransactionCommandHandler(FinanceTrackerDbContext context)
        {
            _context = context;
        }

        
        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            
            decimal finalAmount = request.Amount;

           
            var newTransaction = new FinancialTransaction
            {
                Title = request.Title,
                Amount = finalAmount,
                CurrencyId = request.CurrencyId, // ⬅️ Mutlaka atanmalı
                TransactionTypeId = request.TransactionTypeId,         // ⬅️ Mutlaka atanmalı (1 veya 2)
                Date = DateTime.UtcNow
            };

            // 3. Veritabanına ekle
            await _context.Transactions.AddAsync(newTransaction, cancellationToken);

            // 4. Değişiklikleri kaydet (Bu artık NULL hatası vermemeli)
            await _context.SaveChangesAsync(cancellationToken);

            return newTransaction.Id;
        }
    }
}