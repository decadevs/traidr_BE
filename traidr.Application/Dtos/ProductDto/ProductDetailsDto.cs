using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Application.Dtos.ShopDto;

namespace traidr.Application.Dtos.ProductDto
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }       
        public string Description { get; set; }
        public decimal Price { get; set; }
        public SellerDto Seller { get; set; }
        public ICollection<string> Images { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; }
        public ICollection<ProductElementDto> ProductElements { get; set; }
        

    }
}
