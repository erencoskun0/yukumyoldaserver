using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yukumyolda.Domain.Entities;

namespace yukumyolda.Application.Features.Results.VehicleResults
{
    public class GetByIdVehicleResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Plate { get; set; } = default!;
        public bool IsReady { get; set; } = true;
   
        public string? VehicleTypeName { get; set; }  
        public string? VehicleBodyName { get; set; }  


        public decimal Height { get; set; } = default!;
        public decimal Width { get; set; } = default!;
        public decimal Length { get; set; } = default!;

        public string? TrailerPlate { get; set; }
    }
}
