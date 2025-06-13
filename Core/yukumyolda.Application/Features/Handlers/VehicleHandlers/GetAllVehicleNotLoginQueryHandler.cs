using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.VehicleQueries;
using yukumyolda.Application.Features.Results.ProvinceResults;
using yukumyolda.Application.Features.Results.VehicleResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleHandlers
{
    public class GetAllVehicleNotLoginQueryHandler : IRequestHandler<GetAllVehicleNotLoginQuery, List<GetAllVehicleNotLoginQueryResult>>
    {
        private readonly YukumYoldaContext _context;

        public GetAllVehicleNotLoginQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllVehicleNotLoginQueryResult>> Handle(GetAllVehicleNotLoginQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.UserVehicles.Include(uv => uv.Vehicle).Where(uv => uv.Vehicle.IsReady == true).Include(uv=> uv.User).Include(uv => uv.Vehicle.Province).Include(uv => uv.Vehicle.VehicleType).Include(uv => uv.Vehicle.VehicleBody).AsNoTracking().ToListAsync();

            if (values.Count == 0)
                throw new Exception("Hiç araç bulunamadı.");

            return values.Select(x => new GetAllVehicleNotLoginQueryResult
            {
               Name = x.User.Name,
               Surname = x.User.Surname,
               ProvinceName = x.Vehicle.Province.ProvinceName,
               Plate = x.Vehicle.Plate,
               VehicleBodyName=x.Vehicle.VehicleBody.BodyName,
               VehicleTypeName=x.Vehicle.VehicleType.TypeName,
               Height = x.Vehicle.Height,
               Width = x.Vehicle.Width,
               Length = x.Vehicle.Length,
               TrailerPlate = x.Vehicle.TrailerPlate,
            }).ToList();
        }
    }
}
 