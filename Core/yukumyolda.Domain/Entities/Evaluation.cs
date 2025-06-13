using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yukumyolda.Domain.Common;

namespace yukumyolda.Domain.Entities
{
    public class Evaluation : EntityBase
    {
        public Guid EvaluatorUserId { get; set; }
        public User EvaluatorUser { get; set; } = default!;
        public Guid EvaluatedUserId { get; set; }
        public User EvaluatedUser { get; set; } = default!;
        public Guid LoadId { get; set; }
        public Load Load { get; set; } = default!;
        public int Rating { get; set; } // 0-5 arası
        public string? Comment { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
