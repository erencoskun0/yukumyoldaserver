using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yukumyolda.Domain.Common;

namespace yukumyolda.Domain.Entities
{
    
    public class Load : EntityBase
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public string Description { get; set; } = default!;
        public int LoadStatusId { get; set; } = 0;
        public LoadStatus LoadStatus { get; set; } = default!;

        public DateTime LoadTime { get; set; } = default!;

        // Kalkış ili
        public int DeparturevId { get; set; } //kalkış
        public Province DepartureProvince { get; set; } = default!;

        // Varış ili
        public int DestinationProvinceId { get; set; }   //varış
        public Province DestinationProvince { get; set; } = default!;

        public decimal? Weight { get; set; } //ton 
        public decimal? Length { get; set; } //metre

        public ICollection<UserLoad> UserLoads { get; set; } = new List<UserLoad>();
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
