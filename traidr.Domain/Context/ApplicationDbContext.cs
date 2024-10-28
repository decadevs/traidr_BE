using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traidr.Domain.Models;

namespace traidr.Domain.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }


        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductElement> ProductElements { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }       
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tracking> Trackings { get; set; }        
        public DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // One-to-many relationship for conversations where the user is a seller
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.Seller)
                .WithMany(u => u.Conversations)  // Only one list of conversations in User
                .HasForeignKey(c => c.SellerId)
                .OnDelete(DeleteBehavior.SetNull);  // Prevent cascade delete

            // One-to-many relationship for conversations where the user is a buyer
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.Buyer)
                .WithMany()  // Same single list of conversations
                .HasForeignKey(c => c.BuyerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductCategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }


}
