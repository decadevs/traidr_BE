using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface IProductElementRepository
    {
        Task AddProductElement(ProductElement productElement);
        Task AddProductElementsAsync(IEnumerable<ProductElement> productElements);
        List<ProductElement> GetProductElementsByProductId(int productId);
    }
}