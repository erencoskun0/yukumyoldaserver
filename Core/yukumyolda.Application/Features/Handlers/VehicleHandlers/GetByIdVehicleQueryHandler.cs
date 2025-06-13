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
using yukumyolda.Domain.Entities;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleHandlers
{
    public class GetByIdVehicleQueryHandler : IRequestHandler<GetByIdVehicleQuery, GetByIdVehicleResult>
    {
        private readonly YukumYoldaContext _context;

        public GetByIdVehicleQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<GetByIdVehicleResult> Handle(GetByIdVehicleQuery request, CancellationToken cancellationToken)
        {
            var value = await _context.UserVehicles
             .Where(v => v.VehicleId == request.VehicleId).Include(v => v.Vehicle).Include(v => v.Vehicle.VehicleType).Include(v=> v.User)
             .Select(v => new GetByIdVehicleResult
        {
            Id = v.Vehicle.Id,
            Name = v.User.Name,
            Surname = v.User.Surname,
            PhoneNumber = v.User.PhoneNumber,
            Email = v.User.Email,
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
