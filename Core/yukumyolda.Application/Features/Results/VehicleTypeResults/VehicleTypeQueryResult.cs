using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Application.Features.Results.VehicleTypeResults
{
    public class VehicleTypeQueryResult
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = default!; //tır-kamyon-kamyonet
        public string? IconUrl { get; set; }
    }
}
