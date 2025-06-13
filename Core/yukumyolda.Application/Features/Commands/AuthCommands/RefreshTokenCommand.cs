using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Results.AuthResults;

namespace yukumyolda.Application.Features.Commands.AuthCommands
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResults>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
