using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public int UserId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
