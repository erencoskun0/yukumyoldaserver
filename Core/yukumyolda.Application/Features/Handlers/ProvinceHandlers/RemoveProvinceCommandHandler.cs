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
    public class RemoveProvinceCommandHandler : IRequestHandler<RemoveProvinceCommand>
    {
        private readonly YukumYoldaContext _context;

        public RemoveProvinceCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveProvinceCommand request, CancellationToken cancellationToken)
        {
            var value = await _context.Provinces.FindAsync(request.Id);
            if (value is not null)
            {
                _context.Provinces.Remove(value);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Şehir Bulunamadı!");
            }
        }
    }
}
