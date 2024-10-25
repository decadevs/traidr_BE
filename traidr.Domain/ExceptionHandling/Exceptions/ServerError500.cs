using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class ServerError500 : Exception
    {
        public ServerError500(string msg) : base(msg) { }
    }
}
