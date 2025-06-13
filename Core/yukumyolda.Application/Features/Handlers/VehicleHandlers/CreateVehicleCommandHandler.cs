using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Commands.VehicleCommands;
using yukumyolda.Domain.Entities;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.VehicleHandlers
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand>
    {
        private readonly YukumYoldaContext _context;

        public CreateVehicleCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                Plate = request.Plate,
                VehicleTypeId = request.VehicleTypeId,
                VehicleBodyId = request.VehicleBodyId,
                Height = request.Height,
                Width = request.Width,
                Length = request.Length,
                TrailerPlate = request.TrailerPlate
            };
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            var userVehicle = new UserVehicle
            {
                UserId = request.UserId,
                VehicleId = vehicle.Id,
            };


            await _context.UserVehicles.AddAsync(userVehicle);

            await _context.SaveChangesAsync();

        }
    }
}
 