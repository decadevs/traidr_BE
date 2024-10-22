using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class DatabaseUpdateError : Exception
    {
        public DatabaseUpdateError(string msg) : base(msg) { }
    }
}
