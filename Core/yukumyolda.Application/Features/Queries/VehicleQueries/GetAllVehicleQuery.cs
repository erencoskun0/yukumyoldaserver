using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Results.VehicleResults;

namespace yukumyolda.Application.Features.Queries.VehicleQueries
{
    public class GetAllVehicleQuery : IRequest<List<GetAllVehicleQueryResult>>
    {
    }
}
