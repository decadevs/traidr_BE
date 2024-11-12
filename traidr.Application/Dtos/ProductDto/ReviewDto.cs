using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Application.Dtos.ProductDto
{
    public class ReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }

        public string Reviewer { get; set; }
        public DateTime CommentedAt { get; set; }
    }
}
