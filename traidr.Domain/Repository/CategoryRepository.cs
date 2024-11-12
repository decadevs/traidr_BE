using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Context;
using traidr.Domain.Helper;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Domain.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductCategoryAsync(ProductCategory category)
        {
            await _context.ProductCategories.AddRangeAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCategory>> GetAllCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(int categoryId)
        {         

            return await _context.ProductCategories.FindAsync(categoryId);    

        }

        public async Task<ProductCategory> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == categoryName.ToLower());
        }
    }
}
