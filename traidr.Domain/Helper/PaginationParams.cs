using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Helper
{
    public class PaginationParams
    {
        public int _pageSize = 10;

        public const int MaxPageSize = 100;

        public int PageNumber { get; set; } = 1;

        public int PagesSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
