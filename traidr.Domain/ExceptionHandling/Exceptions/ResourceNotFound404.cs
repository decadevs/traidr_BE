using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.ExceptionHandling.Exceptions
{
    public class ResourceNotFound404 : Exception
    {
        public ResourceNotFound404(string msg) : base(msg) { }
    }
}
