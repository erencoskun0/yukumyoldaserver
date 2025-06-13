using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.LoadQueries;
using yukumyolda.Application.Features.Results.LoadResults;
using yukumyolda.Application.Features.Results.VehicleResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.LoadHandlers
{
    public class GetAllLoadsNotLoginQueryHandler : IRequestHandler<GetAllLoadsNotLoginQuery, List<GetAllLoadsNotLoginQueryResult>>
    {
        private readonly YukumYoldaContext _context;

        public GetAllLoadsNotLoginQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllLoadsNotLoginQueryResult>> Handle(GetAllLoadsNotLoginQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.UserLoads.Include(uv => uv.Load).Where(uv => uv.Load.IsDeleted == false).Include(uv => uv.User).AsNoTracking().ToListAsync();

            if (values.Count == 0)
                throw new Exception("Hiç yük bulunamadı.");

            return values.Select(x => new GetAllLoadsNotLoginQueryResult
            {
                Id = x.LoadId,
                Name = x.User.Name,
                Surname = x.User.Surname,
                Description=x.Load.Description,
                LoadStatus = x.Load.LoadStatus.StateName,
                CreatedDate = x.Load.CreatedDate,
                LoadTime = x.Load.LoadTime,
                Departurev = x.Load.DepartureProvince.ProvinceName,
                DestinationProvince = x.Load.DestinationProvince.ProvinceName,
                Weight = x.Load.Weight,
                Length = x.Load.Length,
            }).ToList();
        }
    }
}
 