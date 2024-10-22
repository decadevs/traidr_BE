using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class ConflictError409 : Exception
    {
        public ConflictError409(string msg) : base(msg) { }
    }
}
