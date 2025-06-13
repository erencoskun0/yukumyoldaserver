using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Application.Features.Results.VehicleResults
{
    public class GetAllVehicleNotLoginQueryResult
    {
       
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Plate { get; set; } = default!;
 

        public string? VehicleTypeName { get; set; }
        public string? VehicleBodyName { get; set; }

        public string? ProvinceName { get; set; }

        public decimal Height { get; set; } = default!;
        public decimal Width { get; set; } = default!;
        public decimal Length { get; set; } = default!;

        public string? TrailerPlate { get; set; }
    }
}
