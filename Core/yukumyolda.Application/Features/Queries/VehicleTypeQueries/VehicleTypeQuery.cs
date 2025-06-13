using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Results.VehicleTypeResults;

namespace yukumyolda.Application.Features.Queries.VehicleTypeQueries
{
    public class VehicleTypeQuery : IRequest<List<VehicleTypeQueryResult>>
    {
    }
}
