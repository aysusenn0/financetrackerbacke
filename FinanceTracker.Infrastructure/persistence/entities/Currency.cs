using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Infrastructure.persistence.entities
{
    // FinanceTracker.Infrastructure/Persistence/Entities/Currency.cs
   
        public class Currency
        {
            public int Id { get; set; } // Birincil Anahtar
            public string? Code { get; set; }
            public string? Name { get; set; }
            public decimal RateToTRY { get; set; }
            public DateTime LastUpdated { get; set; }
        }
    }