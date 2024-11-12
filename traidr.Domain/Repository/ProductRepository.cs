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
            return await _context.Products.Where(p => p.ProductCategoryId == id).Include(p => p.ProductImages).ToListAsync();
        }

        public async Task<Product> FindProductByIdAsync(int productId)
        {

            return await _context.Products
                .Include(p => p.Seller)
                .ThenInclude(s => s.Shop)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategory)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User)
                .Where(p => p.ProductId == productId)
                .Include(p => p.ProductElements)
                .SingleOrDefaultAsync();
           
        }
        
        public async Task<List<Review>> GetProductReviewsAsync(int productId)
        {
            return await _context.Reviews
                .Where(r  => r.ProductId == productId)
                .ToListAsync();           
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _context.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductCategory).ToListAsync();
        }

        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public ProductCategory GetCategoryById(int categoryId)
        {
            try
            {
                var category = _context.ProductCategories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (category != null)
                {
                    return category;
                }
                else
                {
                    Console.WriteLine($"Category with ID {categoryId} not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
