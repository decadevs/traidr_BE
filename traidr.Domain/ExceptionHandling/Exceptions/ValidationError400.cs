using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class ValidationError400 : Exception
    {
        public ValidationError400(string msg) : base(msg) { }
    }
}
