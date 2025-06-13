using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace yukumyolda.Application.Features.Commands.LoadCommands
{
    public class UpdateLoadStateCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public int LoadStateId { get; set; }
    }
}
