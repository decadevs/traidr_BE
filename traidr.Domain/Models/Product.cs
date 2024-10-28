using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }


        [ForeignKey("ProductCategory")]
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public decimal Price { get; set; }     


        [ForeignKey("Seller")]
        public string SellerId { get; set; }

        public AppUser Seller { get; set; }

        public DateTime CreationDate {  get; set; }

        public ICollection<ProductElement> ProductElements { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

        public ICollection<Review> Reviews { get; set; }       

    }
}
