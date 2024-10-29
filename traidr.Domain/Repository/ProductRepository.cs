using Microsoft.EntityFrameworkCore;
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
    public class ProductRepository : IProductRepository
    { 
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddProductImagesAsync(List<ProductImage> productImage)
        {
            await _context.ProductImages.AddRangeAsync(productImage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> FindProductByCategoryIdAsync(int id)
        {
            return await _context.Products.Where(s => s.ProductCategoryId == id).ToListAsync();
        }

        public async Task<Product> FindProductByIdAsync(int productId)
        {

            return await _context.Products.FindAsync(productId);
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
