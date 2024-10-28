using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Enums;

namespace traidr.Domain.Models
{
    public class AppUser : IdentityUser
    { 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public string? ProfilePhoto { get; set; }
        public Shop Shop { get; set; }
        public bool IsSeller { get; set; } = false;
        public ReferralSource? ReferralSource { get; set; }

        public ICollection<Conversation> Conversations { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

    }
}
