using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Domain.Entities
{
    public class LoadStatus
    {
        public int Id { get; set; }
        public string StateName { get; set; } = default!;
        public ICollection<Load> Loads { get; set; } = new List<Load>();
    }
}
