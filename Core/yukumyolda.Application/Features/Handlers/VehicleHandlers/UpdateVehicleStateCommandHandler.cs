using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Commands.VehicleCommands;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleHandlers
{
    public class UpdateVehicleStateCommandHandler : IRequestHandler<UpdateVehicleStateCommand>
    {
        private readonly YukumYoldaContext _context;

        public UpdateVehicleStateCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateVehicleStateCommand request, CancellationToken cancellationToken)
        {
            var userVehicle = await _context.UserVehicles
                      .Include(uv => uv.Vehicle)
                      .FirstOrDefaultAsync(uv => uv.UserId == request.UserId && uv.VehicleId == request.VehicleId, cancellationToken);

            if (userVehicle?.Vehicle is not null)
            {
                var value = userVehicle.Vehicle;

                value.ProvinceId = request.ProvinceId;
                value.IsReady = request.IsReady;
             

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("Araç Bulunamadı!");
            }
        }
    }
}
