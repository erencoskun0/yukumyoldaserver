using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yukumyolda.Domain.Common;

namespace yukumyolda.Domain.Entities
{
    public class Vehicle : EntityBase
    {
        public string Plate { get; set; } = default!;
        public bool IsReady { get; set; } = true;
        public int? ProvinceId { get; set; }
        public Province? Province { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } = default!;
        public int VehicleBodyId { get; set; }
        public VehicleBody VehicleBody { get; set; } = default!;
        public UserVehicle UserVehicle { get; set; } = default!;
        public decimal Height { get; set; } = default!;
        public decimal Width { get; set; } = default!;
        public decimal Length { get; set; } = default!;

        public string? TrailerPlate { get; set; }

    }
}
