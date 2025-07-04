﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Domain.Entities
{
    public class UserVehicle
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = default!;
    }
}
