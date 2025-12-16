using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Features.Transactions
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CurrencyCode { get; set; } // "USD", "TRY"
        public string TypeName { get; set; }     // "Gelir", "Gider"
        public int TransactionTypeId { get; set; }
        //public object TypeId { get; internal set; }

        // Android'de Yeşil/Kırmızı ayrımı için 
        //public int TypeId { get; set; }

    }
}
