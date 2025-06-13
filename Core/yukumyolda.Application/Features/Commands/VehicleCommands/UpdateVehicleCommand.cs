using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Domain.Entities;

namespace yukumyolda.Application.Features.Commands.VehicleCommands
{
    public class UpdateVehicleCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        
        public string Plate { get; set; } = default!;
        public bool IsReady { get; set; } = true;
        public int VehicleTypeId { get; set; } 
        public int VehicleBodyId { get; set; }  
        public decimal Height { get; set; } = default!;
        public decimal Width { get; set; } = default!;
        public decimal Length { get; set; } = default!;

        public string? TrailerPlate { get; set; }

    }
}
