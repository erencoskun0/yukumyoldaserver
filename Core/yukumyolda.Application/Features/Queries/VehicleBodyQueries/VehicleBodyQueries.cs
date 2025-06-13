using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Results.VehicleBodyResults;

namespace yukumyolda.Application.Features.Queries.VehicleBodyQueries
{
    public class VehicleBodyQueries : IRequest<List<VehicleBodyQueriesResult>>
    {
    }
}
