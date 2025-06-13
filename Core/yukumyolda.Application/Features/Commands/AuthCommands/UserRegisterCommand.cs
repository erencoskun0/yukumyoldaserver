using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace yukumyolda.Application.Features.Commands.AuthCommands
{
    public class UserRegisterCommand : IRequest<Unit>
    {
        public string Name { get; set; } = default!;
        public string SurName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Guid RoleId { get; set; }
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;

    }
}
