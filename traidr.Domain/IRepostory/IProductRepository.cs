using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task AddProductImagesAsync(List<ProductImage>  productImage);
        Task<List<Review>> GetProductReviewsAsync(int productId);
        Task<Product> FindProductByIdAsync(int id);
        Task<List<Product>> FindProductByCategoryIdAsync(int id);
        Task<List<Product>> GetAllProductAsync();
        Task AddReviewAsync(Review review);

        ProductCategory GetCategoryById(int categoryId);
    }
}