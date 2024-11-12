using traidr.Domain.Helper;
using traidr.Domain.Models;

namespace traidr.Domain.IRepostory
{
    public interface IShopRepository
    {
        Task AddShopAsync(Shop shop);
        Task<List<Shop>> GetAllShopsAsync();
        Task<Shop> GetShopBySellerIdAsync(string sellerId);
        Task<ShopAndProductDto> GetShopAndProductAsync(string sellerId);
        Task UpdateShopAsync(Shop shop);
    }
}