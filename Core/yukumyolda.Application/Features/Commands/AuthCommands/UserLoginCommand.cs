using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using yukumyolda.Application.Features.Results.AuthResults;

namespace yukumyolda.Application.Features.Commands.AuthCommands
{
    public class UserLoginCommand : IRequest<UserLoginResults>
    {
        public string? Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
