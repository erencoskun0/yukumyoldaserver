using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace yukumyolda.Application.Features.Commands.LoadCommands
{
    public class UpdateLoadCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; } = default!;
        public DateTime LoadTime { get; set; } = default!;
        public int DeparturevId { get; set; } //kalkış
        public int DestinationProvinceId { get; set; }   //varış
        public decimal? Weight { get; set; } //ton 
        public decimal? Length { get; set; } //metre
    }
}
