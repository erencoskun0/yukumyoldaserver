using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Results.ProvinceResults;

namespace yukumyolda.Application.Features.Queries.ProvinceQueries
{
    public class GetProvinceQuery : IRequest<List<GetProvinceQueryResults>>
    {
    }
}
