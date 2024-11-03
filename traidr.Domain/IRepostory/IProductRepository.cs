using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task AddProductImagesAsync(List<ProductImage>  productImage);

        Task<Product> FindProductByIdAsync(int id);
        Task<List<Product>> FindProductByCategoryIdAsync(int id);
        Task<List<Product>> GetAllProductAsync();
    }
}