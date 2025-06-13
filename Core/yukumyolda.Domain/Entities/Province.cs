using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Domain.Entities
{
    
    public class Province
    {
        public int Id { get; set; }  
        public string ProvinceName { get; set; } = default!;

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public ICollection<Load> DepartureLoads { get; set; } = new List<Load>();
        public ICollection<Load> DestinationLoads { get; set; } = new List<Load>();
    }
}
