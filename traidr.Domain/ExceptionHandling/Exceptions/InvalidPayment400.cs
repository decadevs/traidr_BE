using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class InvalidPayment400 : Exception
    {
        public InvalidPayment400(string msg) : base(msg) { }
    }
}
