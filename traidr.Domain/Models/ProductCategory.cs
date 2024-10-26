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

        public string CategoryName { get; set; }

        [ForeignKey("ProductCategory")]
        public int? ParentCategoryId { get; set; }
        public ProductCategory ParentCategory { get; set; }   

        public ICollection<ProductCategory> SubCategories { get; set; }

        //public ICollection<Product> Products { get; set; }
    }
}
