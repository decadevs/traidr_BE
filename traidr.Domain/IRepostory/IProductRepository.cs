using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);
        Task AddProductImagesAsync(List<ProductImage>  productImage);
    }
}