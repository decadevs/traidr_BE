using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Enums;

namespace traidr.Application.Dtos.UserDto
{
    public class UserSignupDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public ReferralSource ReferralSource { get; set; }
    }
}
