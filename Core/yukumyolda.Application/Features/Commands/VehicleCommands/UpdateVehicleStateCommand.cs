using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace yukumyolda.Application.Features.Commands.VehicleCommands
{
    public class UpdateVehicleStateCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid VehicleId { get; set; }
        public int ProvinceId { get; set; }
        public bool IsReady { get; set; }
    }
}
