using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Enums;

namespace traidr.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? ShopName { get; set; }
        public bool IsSeller { get; set; } = false;
    }
}
