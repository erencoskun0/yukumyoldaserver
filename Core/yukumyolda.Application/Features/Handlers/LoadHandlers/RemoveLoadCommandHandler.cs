using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Commands.LoadCommands;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.LoadHandlers
{
    public class RemoveLoadCommandHandler : IRequestHandler<RemoveLoadCommand>
    {
        private readonly YukumYoldaContext _context;

        public RemoveLoadCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveLoadCommand request, CancellationToken cancellationToken)
        {
            var userLoad = await _context.UserLoads
          .Include(uv => uv.Load)
          .FirstOrDefaultAsync(uv => uv.UserId == request.UserId && uv.LoadId == request.Id, cancellationToken);

            if (userLoad?.Load is not null)
            {
                var value = userLoad.Load;

                value.IsDeleted = true;


                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("Yük Bulunamadı!");
            }
        }
    }
}
