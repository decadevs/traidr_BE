using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class AppUser
    {
        
        public string Gender { get; set; }
        public int Age { get; set; }
        public string ProfilePhoto { get; set; }
        public string ShopName { get; set; }
        public bool IsSeller { get; set; }
    }
}
