using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Application.Dtos.ProductDto
{
    public class ProductListingDto
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public ICollection<IFormFile> ProductImage { get; set; }

        //public ICollection<ProductElementDto> ProductElementDto { get; set; }
     

    }
}
