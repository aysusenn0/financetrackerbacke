using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Infrastructure.persistence.entities
{
    namespace FinanceTracker.Infrastructure.Persistence.Entities
    {
        public class TransactionType
        {
            public int Id { get; set; } // Birincil Anahtar
            public string Name { get; set; }
        }
    }
}