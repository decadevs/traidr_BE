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


        [ForeignKey("ProductCategories")]
        public string ProductCategory { get; set; }
        public ProductCategory ProductCategories { get; set; }


        public decimal Price { get; set; }


     

        [ForeignKey("User")]
        public string UserId { get; set; }

        public AppUser User { get; set; }

        public DateTime CreationDate {  get; set; }

        public ICollection<ProductElement> ProductElements { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

       

    }
}
