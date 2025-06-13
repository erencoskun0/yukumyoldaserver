using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace yukumyolda.Application.Features.Commands.ProvinceCommands
{
    public class UpdateProvinceCommand : IRequest
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; } = default!;
    }
}
  