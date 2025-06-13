using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Queries.LoadQueries;
using yukumyolda.Application.Features.Results.LoadResults;
using yukumyolda.Application.Features.Results.VehicleResults;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.LoadHandlers
{
    public class GetByIdLoadQueryHandler : IRequestHandler<GetByIdLoadQuery, GetByIdLoadQueryResult>
    {
        private readonly YukumYoldaContext _context;

        public GetByIdLoadQueryHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task<GetByIdLoadQueryResult> Handle(GetByIdLoadQuery request, CancellationToken cancellationToken)
        {
            var value = await _context.UserLoads
                   .Where(v => v.LoadId == request.LoadId).Include(v => v.Load).Include(v => v.User)
                   .Select(x => new GetByIdLoadQueryResult
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
                   })
              .FirstOrDefaultAsync(cancellationToken);

            if (value == null)
                throw new Exception("Yük bulunamadı!");

            return value;
        }
    }
}
