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
    public class UpdateProvinceCommandHandler : IRequestHandler<UpdateProvinceCommand>
    {
        private readonly YukumYoldaContext _context;

        public UpdateProvinceCommandHandler(YukumYoldaContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
        {
            var value = await _context.Provinces.FindAsync(request.Id);
            if (value is not null)
            {
                value.ProvinceName = request.ProvinceName;
                await _context.SaveChangesAsync();
            }else
            {
                throw new Exception("Şehir Bulunamadı!");
            }
        }
    }
}
