using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FinanceTracker.Application.Features.Currencies
{
    
    public class GetCurrenciesQuery : IRequest<List<CurrencyDto>>
    {
    }
}