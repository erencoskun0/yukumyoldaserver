using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace yukumyolda.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {

        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public UserVehicle? UserVehicle { get; set; }
        public ICollection<UserLoad>? UserLoads { get; set; } = new List<UserLoad>(); 

        public ICollection<Evaluation> GivenEvaluations { get; set; } = new List<Evaluation>(); // Kullanıcının başkalarına verdiği puanlar
        public ICollection<Evaluation> ReceivedEvaluations { get; set; } = new List<Evaluation>(); // Kullanıcının aldığı puanlar

    }
}
