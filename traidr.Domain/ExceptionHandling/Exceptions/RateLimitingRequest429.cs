using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class RateLimitingRequest429 : Exception
    {
        public RateLimitingRequest429(string msg) : base(msg) { }
    }
}
