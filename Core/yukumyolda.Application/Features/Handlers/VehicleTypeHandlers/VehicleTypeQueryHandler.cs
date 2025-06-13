using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.VehicleTypeQueries;
using yukumyolda.Application.Features.Results.VehicleBodyResults;
using yukumyolda.Application.Features.Results.VehicleTypeResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleTypeHandlers
{
    public class VehicleTypeQueryHandler : IRequestHandler<VehicleTypeQuery, List<VehicleTypeQueryResult>>
    {
        private readonly YukumYoldaContext _context;

        public VehicleTypeQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleTypeQueryResult>> Handle(VehicleTypeQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.VehicleTypes.AsNoTracking().ToListAsync();

            if (values.Count == 0)
                throw new Exception("Hiç Tip bulunamadı.");

            return values.Select(x => new VehicleTypeQueryResult
            {
                Id = x.Id,
                TypeName = x.TypeName,
                IconUrl = x.IconUrl,
            }).ToList();
        }
    }
}
