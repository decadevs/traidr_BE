using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class AuthorizationError403 : Exception
    {
        public AuthorizationError403(string msg) : base(msg) { }
    }
}
