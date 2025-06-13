using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Results.LoadStatusResult;

namespace yukumyolda.Application.Features.Queries.LoadStatusQueries
{
    public class LoadStatusQuery : IRequest<List<LoadStatusQueryResult>>
    {
    }
}
