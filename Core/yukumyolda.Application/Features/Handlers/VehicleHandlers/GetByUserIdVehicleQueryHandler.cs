using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.VehicleQueries;
using yukumyolda.Application.Features.Results.VehicleResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleHandlers
{
    public class GetByUserIdVehicleQueryHandler : IRequestHandler<GetByUserIdVehicleQuery, GetByUserIdVehicleQueryResult>
    {
        private readonly YukumYoldaContext _context;

        public GetByUserIdVehicleQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<GetByUserIdVehicleQueryResult> Handle(GetByUserIdVehicleQuery request, CancellationToken cancellationToken)
        {
            var value = await _context.UserVehicles
            .Where(uv => uv.UserId == request.UserId)
            .Include(uv => uv.Vehicle).Include(uv => uv.Vehicle.VehicleType).Include(uv => uv.Vehicle.VehicleBody)
            .Select(v => new GetByUserIdVehicleQueryResult
         {
             Id = v.Vehicle.Id,
             Plate = v.Vehicle.Plate,
             VehicleTypeName = v.Vehicle.VehicleType.TypeName,
             VehicleBodyName = v.Vehicle.VehicleBody.BodyName,
             Height = v.Vehicle.Height,
             Width = v.Vehicle.Width,
             Length = v.Vehicle.Length,
             TrailerPlate = v.Vehicle.TrailerPlate
         })
        .FirstOrDefaultAsync(cancellationToken);

            if (value == null)
                throw new Exception("Araç bulunamadı!");

            return value;

        }
    }
    }

