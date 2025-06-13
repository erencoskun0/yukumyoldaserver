using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Commands.LoadCommands;
using yukumyolda.Domain.Entities;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.LoadHandlers
{
    public class CreateLoadCommandHandler : IRequestHandler<CreateLoadCommand>
    {
        private readonly YukumYoldaContext _context;

        public CreateLoadCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateLoadCommand request, CancellationToken cancellationToken)
        {
            var load = new Load
            {
                UserId = request.UserId,
                Description = request.Description,
                DeparturevId = request.DeparturevId,
                DestinationProvinceId = request.DestinationProvinceId,
                LoadTime = request.LoadTime,
                Length = request.Length,
                Weight = request.Weight,
                 
            };
            await _context.Loads.AddAsync(load);
            await _context.SaveChangesAsync();

            var userLoad = new UserLoad
            {
                UserId = request.UserId,
                LoadId = load.Id,
            };


            await _context.UserLoads.AddAsync(userLoad);

            await _context.SaveChangesAsync();
        }
    }
}
