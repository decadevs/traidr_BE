using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Models;

namespace traidr.Application.Dtos.ShopDto
{
    public class GetShopDto
    {

        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
