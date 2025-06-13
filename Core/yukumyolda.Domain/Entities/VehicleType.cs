using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Domain.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = default!; //tır-kamyon-kamyonet
        public string? IconUrl { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
