using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Commands.LoadCommands;
using yukumyolda.Domain.Entities;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.LoadHandlers
{
    public class UpdateLoadCommandHandler : IRequestHandler<UpdateLoadCommand>
    {
        private readonly YukumYoldaContext _context;

        public UpdateLoadCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateLoadCommand request, CancellationToken cancellationToken)
        {
            var userLoad = await _context.UserLoads
              .Include(uv => uv.Load)
              .FirstOrDefaultAsync(uv => uv.UserId == request.UserId && uv.LoadId == request.Id, cancellationToken);

            if (userLoad?.Load is not null)
            {
                var value = userLoad.Load;

                value.Description = request.Description;
                value.DeparturevId = request.DeparturevId;
                value.DestinationProvinceId = request.DestinationProvinceId;
                value.LoadTime = request.LoadTime;
                value.Length = request.Length;
                value.Weight = request.Weight;

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("Yük Bulunamadı!");
            }
        }
    }
}
