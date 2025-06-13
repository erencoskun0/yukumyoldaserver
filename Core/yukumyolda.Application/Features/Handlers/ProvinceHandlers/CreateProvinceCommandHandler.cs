using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Commands.ProvinceCommands;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.ProvinceHandlers
{
    public class CreateProvinceCommandHandler : IRequestHandler<CreateProvinceCommand>
    {
        private readonly YukumYoldaContext _context;

        public CreateProvinceCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
        {
          await  _context.Provinces.AddAsync(new Domain.Entities.Province
            {
                ProvinceName = request.ProvinceName,
            });
            await _context.SaveChangesAsync();
        }
    }
}
