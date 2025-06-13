using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.VehicleBodyQueries;
using yukumyolda.Application.Features.Results.ProvinceResults;
using yukumyolda.Application.Features.Results.VehicleBodyResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleBodyHandlers
{
    public class VehicleBodyQueriesHandler : IRequestHandler<VehicleBodyQueries, List<VehicleBodyQueriesResult>>
    {
        private readonly YukumYoldaContext _context;

        public VehicleBodyQueriesHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleBodyQueriesResult>> Handle(VehicleBodyQueries request, CancellationToken cancellationToken)
        {
            var values = await _context.VehicleBodies.AsNoTracking().ToListAsync();

            if (values.Count == 0)
                throw new Exception("Hiç Yapı bulunamadı.");

            return values.Select(x => new VehicleBodyQueriesResult
            {
                Id = x.Id, 
                BodyName = x.BodyName,
                IconUrl = x.IconUrl,
            }).ToList();
        }
    }
}
