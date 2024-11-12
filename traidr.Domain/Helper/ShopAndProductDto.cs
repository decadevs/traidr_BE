using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Dtos.ProductDto;
using traidr.Domain.Models;

namespace traidr.Domain.Helper
{
    public class ShopAndProductDto
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public List<ProductInventoryDto> Products { get; set; }
    }
}
