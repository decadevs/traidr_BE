using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Models;

namespace traidr.Application.Dtos.ProductDto
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        //public string SubCategoryName { get; set; } 
        //public List<ProductCategoryDto> SubCategories { get; set; }
    }
}
