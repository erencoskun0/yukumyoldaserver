using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Application.Features.Results.VehicleBodyResults
{
    public class VehicleBodyQueriesResult
    {
        public int Id { get; set; }
        public string BodyName { get; set; } = default!;
        public string? IconUrl { get; set; }

    }
}
