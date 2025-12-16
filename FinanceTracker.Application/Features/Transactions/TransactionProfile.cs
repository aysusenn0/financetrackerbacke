using AutoMapper;
using FinanceTracker.Infrastructure.Persistence.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Features.Transactions
{
    public class TransactionProfile:Profile
    {
        public TransactionProfile()
        {
            
            CreateMap<FinancialTransaction, TransactionDto>()
               
                .ForMember(dest => dest.CurrencyCode,
                           opt => opt.MapFrom(src => src.Currency.Code))

                
                .ForMember(dest => dest.TypeName,
                           opt => opt.MapFrom(src => src.TransactionType.Name))

              
                .ForMember(dest => dest.TransactionTypeId,
                           opt => opt.MapFrom(src => src.TransactionType.Id));
               
        }
    }
}
