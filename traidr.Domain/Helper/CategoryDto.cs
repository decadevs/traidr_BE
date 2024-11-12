using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Helper
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public List<string> Subcategories { get; set; }
    }
}
