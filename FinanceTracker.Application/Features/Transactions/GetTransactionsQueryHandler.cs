using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions; // ProjectTo için
using FinanceTracker.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Features.Transactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
    {
       
            private readonly FinanceTrackerDbContext _context;
            private readonly IMapper _mapper;

            public GetTransactionsQueryHandler(FinanceTrackerDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
            {
                
                var transactions = await _context.Transactions
                   
                    .Include(t => t.Currency)
                   
                    .Include(t => t.TransactionType)
                    
                    .OrderByDescending(t => t.Date)
                    
                    .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                    
                    .ToListAsync(cancellationToken);

                return transactions;
            }
        }
    }
