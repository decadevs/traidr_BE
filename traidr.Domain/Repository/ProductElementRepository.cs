using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Context;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Domain.Repository
{
    public class ProductElementRepository : IProductElementRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductElementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductElement(ProductElement productElement)
        {
            _context.ProductElements.Add(productElement);
            await _context.SaveChangesAsync();
        }

        public async Task AddProductElementsAsync(IEnumerable<ProductElement> productElements)
        {
            _context.ProductElements.AddRange(productElements);
            await _context.SaveChangesAsync();
        }

    }
}
