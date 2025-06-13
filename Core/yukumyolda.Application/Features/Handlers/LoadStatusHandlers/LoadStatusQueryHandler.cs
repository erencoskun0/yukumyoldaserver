using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.LoadStatusQueries;
using yukumyolda.Application.Features.Results.LoadStatusResult;
using yukumyolda.Application.Features.Results.VehicleBodyResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.LoadStatusHandlers
{
    public class LoadStatusQueryHandler : IRequestHandler<LoadStatusQuery, List<LoadStatusQueryResult>>
    {
        private readonly YukumYoldaContext _context;

        public LoadStatusQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<List<LoadStatusQueryResult>> Handle(LoadStatusQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.LoadStatuses.AsNoTracking().ToListAsync();

            if (values.Count == 0)
                throw new Exception("Hiç Durum bulunamadı.");

            return values.Select(x => new LoadStatusQueryResult
            {
                Id = x.Id,
                StateName= x.StateName,
            }).ToList();
        }
    }
}
