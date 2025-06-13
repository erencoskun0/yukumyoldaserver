using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.LoadQueries;
using yukumyolda.Application.Features.Results.LoadResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.LoadHandlers
{
    public class GetByIdUserLoadsQueryHandler : IRequestHandler<GetByIdUserLoadsQuery, List<GetByIdUserLoadsQueryResult>>
    {
        private readonly YukumYoldaContext _context;

        public GetByIdUserLoadsQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<List<GetByIdUserLoadsQueryResult>> Handle(GetByIdUserLoadsQuery request, CancellationToken cancellationToken)
        {
            var userLoad = await _context.UserLoads
                  .AsNoTracking()
                  .FirstOrDefaultAsync(ul => ul.LoadId == request.LoadId, cancellationToken);

            if (userLoad == null)
                throw new Exception("Yük bulunamadı.");

            var values = await _context.UserLoads
                .Where(ul => ul.UserId == userLoad.UserId && ul.Load.IsDeleted == false)
                .Include(ul => ul.Load)
                .ThenInclude(l => l.LoadStatus)
                .Include(ul => ul.Load.DepartureProvince)
                .Include(ul => ul.Load.DestinationProvince)
                .Include(ul => ul.User)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (values.Count == 0)
                throw new Exception("Hiç yük bulunamadı.");

            return values.Select(x => new GetByIdUserLoadsQueryResult
            {
                Id = x.LoadId,
                Name = x.User.Name,
                Surname = x.User.Surname,
                Email = x.User.Email,
                PhoneNumber = x.User.PhoneNumber,
                Description = x.Load.Description,
                LoadStatus = x.Load.LoadStatus.StateName,
                CreatedDate = x.Load.CreatedDate,
                LoadTime = x.Load.LoadTime,
                Departurev = x.Load.DepartureProvince.ProvinceName,
                DestinationProvince = x.Load.DestinationProvince.ProvinceName,
                Weight = x.Load.Weight,
                Length = x.Load.Length,
            }).ToList();
        }
    }
}
