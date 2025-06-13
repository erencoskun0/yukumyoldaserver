using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Domain.Entities
{
    public class VehicleBody
    {
        public int Id { get; set; }
        public string BodyName { get; set; } = default!;
        public string? IconUrl { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
