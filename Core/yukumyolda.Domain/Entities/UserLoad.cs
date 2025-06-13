using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Domain.Entities
{
    public class UserLoad
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid LoadId { get; set; }

        public Load Load { get; set; } = default!;

       

    }
}
