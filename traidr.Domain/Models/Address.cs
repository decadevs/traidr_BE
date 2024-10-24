﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public AppUser User { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
