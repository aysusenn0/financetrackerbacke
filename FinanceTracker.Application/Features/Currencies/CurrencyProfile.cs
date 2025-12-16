using AutoMapper;
using FinanceTracker.Infrastructure.persistence.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Features.Currencies
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            // Kaynak: Currency, Hedef: CurrencyDto
            CreateMap<Currency, CurrencyDto>();
        }
    }
}