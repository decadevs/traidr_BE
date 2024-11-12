using traidr.Domain.Helper;
using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface ICategoryRepository
    {
        Task<List<ProductCategory>> GetAllCategoriesAsync();
        Task<ProductCategory> GetCategoryByIdAsync(int categoryId);
        Task AddProductCategoryAsync(ProductCategory category);
        Task<ProductCategory> GetCategoryByNameAsync(string categoryName);



    }
}