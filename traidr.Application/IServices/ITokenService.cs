using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Models;

namespace traidr.Application.IServices
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
