using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Helper
{
    public class QueryObject
    {
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? SortBy { get; set; }
        public bool? IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }
}
