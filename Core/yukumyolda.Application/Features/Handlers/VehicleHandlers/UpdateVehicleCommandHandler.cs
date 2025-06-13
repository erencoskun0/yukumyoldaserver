using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Commands.VehicleCommands;
using yukumyolda.Domain.Entities;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleHandlers
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>
    {
        private readonly YukumYoldaContext _context;

        public UpdateVehicleCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var userVehicle = await _context.UserVehicles
            .Include(uv => uv.Vehicle)
            .FirstOrDefaultAsync(uv => uv.UserId == request.UserId && uv.VehicleId == request.Id, cancellationToken);

            if (userVehicle?.Vehicle is not null)
            {
                var value = userVehicle.Vehicle;

                value.Plate = request.Plate;
                value.VehicleTypeId = request.VehicleTypeId;
                value.VehicleBodyId = request.VehicleBodyId;
                value.Height = request.Height;
                value.Width = request.Width;
                value.Length = request.Length;
                value.TrailerPlate = request.TrailerPlate;

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("Araç Bulunamadı!");
            }
        }
    }
}
 