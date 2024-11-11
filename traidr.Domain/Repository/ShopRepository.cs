using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Context;
using traidr.Domain.Dtos.ProductDto;
using traidr.Domain.Helper;
using traidr.Domain.IRepostory;
using traidr.Domain.Models;

namespace traidr.Domain.Repository
{
    public partial class ShopRepository : IShopRepository
    {
        private readonly ApplicationDbContext _context;

        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddShopAsync(Shop shop)
        {
            await _context.Shops.AddAsync(shop);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Shop>> GetAllShopsAsync()
        {
            return await _context.Shops.ToListAsync();
        }
        public async Task<Shop> GetShopBySellerIdAsync(string sellerId)
        {
            return await _context.Shops.FirstOrDefaultAsync(c => c.SellerId == sellerId);
        }
        
        public async Task<ShopAndProductDto> GetShopAndProductAsync(string sellerId)
        {

            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.SellerId == sellerId);
            if (shop == null)
            {
                return null;
            }

            // Fetch products with their associated images
            var products = await _context.Products
                .Where(p => p.SellerId == sellerId)
                .Include(p => p.ProductImages)
                .ToListAsync();

            // Map the products to ProductDto
            var productDtos = products.Select(p => new ProductInventoryDto
            {
                Id = p.ProductId,
                Name = p.ProductName,
                Price = p.Price,
                CategoryId = p.ProductCategoryId,
                Description = p.ProductDescription,
                Images = p.ProductImages.Select(pi => pi.ImageUrl).ToList()
            }).ToList();

            return new ShopAndProductDto
            {
                Id = shop.Id,
                ShopName = shop.ShopName,
                Description = shop.Description,
                Products = productDtos,  
            };
        }

        public async Task UpdateShopAsync(Shop shop)
        {
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync();
        }


    }
}
