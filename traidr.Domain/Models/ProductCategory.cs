using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Domain.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product {  get; set; }


        [ForeignKey("ProductCategory")]
        public int ParentCategoryId { get; set; }
        public ProductCategory ParentCategory { get; set; }


   

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
