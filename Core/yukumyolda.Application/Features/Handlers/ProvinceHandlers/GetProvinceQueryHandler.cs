using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.ProvinceQueries;
using yukumyolda.Application.Features.Results.ProvinceResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.ProvinceHandlers
{
    public class GetProvinceQueryHandler : IRequestHandler<GetProvinceQuery, List<GetProvinceQueryResults>>
    {
        private readonly YukumYoldaContext _context;

        public GetProvinceQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<List<GetProvinceQueryResults>> Handle(GetProvinceQuery request, CancellationToken cancellationToken)
        {
            var values = await _context.Provinces.AsNoTracking().ToListAsync();

            if (values.Count == 0)
                throw new Exception("Hiç şehir bulunamadı.");

            return values.Select(x => new GetProvinceQueryResults
            {
                Id = x.Id,
                ProvinceName = x.ProvinceName,
            }).ToList();
        }
    }
}
