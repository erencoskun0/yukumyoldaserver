using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace yukumyolda.Application.Features.Commands.ProvinceCommands
{
    public class CreateProvinceCommand : IRequest
    {
 
        public string ProvinceName { get; set; } = default!;

 
    }
}
