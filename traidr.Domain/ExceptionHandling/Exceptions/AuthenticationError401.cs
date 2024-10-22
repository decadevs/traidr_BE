using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class AuthenticationError401 : Exception
    {
        public AuthenticationError401(string msg) : base(msg) { }
    }
}
