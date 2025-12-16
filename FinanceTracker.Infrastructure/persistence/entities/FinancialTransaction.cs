using FinanceTracker.Infrastructure.persistence.entities;
using FinanceTracker.Infrastructure.persistence.entities.FinanceTracker.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinanceTracker.Infrastructure.Persistence.entities
{
    public class FinancialTransaction 
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime Date { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}