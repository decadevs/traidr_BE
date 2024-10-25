﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Infrastructure.Pagination
{
    public class PaginationRequest
    {
        private int _pageSize = 10;
        private const int MaxPageSize = 100;

        public int PageNumver { get; set; } = 1;

        public int PagesSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
