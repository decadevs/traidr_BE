using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using traidr.Domain.Models;
using traidr.Domain.Enums;
using System.Runtime.Intrinsics.X86;
namespace traidr.Domain.Context.PreSeeding
{
    public class Seeding
    {


        public static async Task SeedData(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            await SeedAppUserAndRoles(roleManager, userManager);
            //await ProductSeedData.SeedProductData(context);
            await SeedAllDataAsync(context);
        }

        public static async Task SeedAppUserAndRoles(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            // Seed Roles
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            // Seed Admin User
            if (userManager.Users.All(u => u.UserName.Substring(1) != "izuchukwu"))
            {
                var adminUser = new AppUser
                {
                    UserName = "@" + "izuchukwu",
                    NormalizedUserName = "@" + "IZUCHUKWU",
                    Email = "sircent100@gmai.com",
                    NormalizedEmail = "SIRCENT100@GMAI.COM",
                    Gender = Gender.Male,
                    Age = 30,
                };

                await userManager.CreateAsync(adminUser, "AdminPassword123!");
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            }

            // Seed a Regular User
            if (userManager.Users.All(u => u.UserName.Substring(1) != "fabian"))
            {
                var normalUser = new AppUser
                {
                    UserName = "@" + "fabian",
                    NormalizedUserName = "@" + "FABIAN",
                    Email = "fabbenco97@gmail.com",
                    NormalizedEmail = "FABBENCO97@GMAIL.COM",
                    Gender = Gender.Male,
                    Age = 28,
                };

                await userManager.CreateAsync(normalUser, "UserPassword123!");
                await userManager.AddToRoleAsync(normalUser, UserRoles.User);
            }

            if (userManager.Users.All(u => u.UserName.Substring(1) != "precious"))
            {
                var normalUser = new AppUser
                {
                    UserName = "@" + "precious",
                    NormalizedUserName = "@" + "PRECIOUS",
                    Email = "precious@gmail.com",
                    NormalizedEmail = "PRECIOUS@GMAIL.COM",
                    Gender = Gender.Female,
                    Age = 35,
                    //IsSeller = true,
                    //Shop = null
                };

                await userManager.CreateAsync(normalUser, "UserPassword100!");
                await userManager.AddToRoleAsync(normalUser, UserRoles.User);
            }
        }


        public static async Task SeedAllDataAsync(ApplicationDbContext context)
        {
            string user1Id = "31c29d76-181a-4520-a74d-0de97bfcde33";
            string user2Id = "8a1d5819-d626-4134-8bed-3549aafb2491";
            string user3Id = "df31d370-0ce9-47de-ba63-c56670e991dd";


            // Addresses
            if (!context.Addresses.Any())
            {
                var addresses = new List<Address>
                {
                    new Address
                    {
                        UserId = user1Id,
                        Street = "12 Market St",
                        City = "Ikeja",
                        State = "Lagos"
                    },
                    new Address
                    {
                        UserId = user2Id,
                        Street = "45 Elm St",
                        City = "Gwarimpa",
                        State = "Abuja"
                    },
                    new Address
                    {
                        UserId =  user3Id,
                        Street = "78 Oak St",
                        City = "Ikorodu",
                        State = "Lagos"
                    }
                };
                context.Addresses.AddRange(addresses);
                await context.SaveChangesAsync();
            }


            // Product Categories
            if (!context.ProductCategories.Any())
            {
                var categories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        CategoryName = "Electronics"
                    },
                    new ProductCategory
                    {
                        CategoryName = "Home Appliances",
                        ParentCategoryId = 1
                    },
                    new ProductCategory
                    {
                        CategoryName = "Groceries"
                    },
                    new ProductCategory
                    {
                        CategoryName = "Provisions",
                        ParentCategoryId = 2
                    }
                };
                context.ProductCategories.AddRange(categories);
                await context.SaveChangesAsync();

            }

            // Products
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {                    
                    new Product
                    {
                        ProductName = "Smartphone",
                        ProductDescription = "High-end smartphone",
                        ProductCategoryId = 1,
                        SellerId = user2Id,
                        Price = 699.99M,
                        CreationDate = DateTime.UtcNow.AddMonths(-2),
                        ProductElements = new List<ProductElement>
                        {
                            new ProductElement
                            {
                                ProductId = 1,
                                QuantityInStock = 50,
                                Sku = 1001
                            },
                            new ProductElement
                            {
                                ProductId = 1,
                                QuantityInStock = 30,
                                Sku = 1002
                            }
                        },
                        ProductImages = new List<ProductImage>
                        {
                            new ProductImage
                            {
                                ProductId = 1,
                                ImageUrl = "smartphone1.jpg",
                                publicId = "img1"
                            },
                            new ProductImage
                            {
                                ProductId = 1,
                                ImageUrl = "smartphone2.jpg",
                                publicId = "img2"
                            }
                        },
                        Reviews = new List<Review>
                        {
                            new Review
                            {
                                ProductId= 1,
                                UserId = user1Id,
                                Comment = "Great product!", Rating = 5,
                                Date = DateTime.UtcNow.AddDays(-5) },
                            new Review
                            {
                                ProductId = 1,
                                UserId = user3Id,
                                Comment = "Good value for the money",
                                Rating = 4,
                                Date = DateTime.UtcNow.AddDays(-10)
                            }
                        }
                    },
                    new Product
                    {
                        ProductName = "Blender",
                        ProductDescription = "High-power blender for smoothies",
                        ProductCategoryId = 2,
                        SellerId = user2Id,
                        Price = 89.99M,
                        CreationDate = DateTime.UtcNow.AddMonths(-1),
                        ProductElements = new List<ProductElement>
                        {
                            new ProductElement
                            {
                                ProductId = 2,
                                QuantityInStock = 100,
                                Sku = 2001
                            }
                        },
                        ProductImages = new List<ProductImage>
                        {
                            new ProductImage
                            {
                                ProductId = 2,
                                ImageUrl = "blender.jpg",
                                publicId = "img3"
                            }
                        },
                        Reviews = new List<Review>
                        {
                            new Review
                            {
                                ProductId = 2,
                                UserId = user1Id,
                                Comment = "Very useful",
                                Rating = 4,
                                Date = DateTime.UtcNow.AddDays(-3)
                            }
                        }
                    }
                };
                context.Products.AddRange(products);
                await context.SaveChangesAsync();

            }



            // Orders
            if (!context.Orders.Any())
            {
                var product1 = context.Products.FirstOrDefault(p => p.ProductName == "Smartphone");
                var product2 = context.Products.FirstOrDefault(p => p.ProductName == "Blender");
               
                var category1 = context.ProductCategories.FirstOrDefault(c => c.CategoryName == "Electronics");
                var category2 = context.ProductCategories.FirstOrDefault(c => c.CategoryName == "Groceries");
                var address1 = context.Addresses.FirstOrDefault(a => a.UserId == user1Id);
                var address2 = context.Addresses.FirstOrDefault(a => a.UserId == user2Id);

                var orders = new List<Order>
                {
                    new Order
                    {
                        UserId = user1Id,
                        CategoryId = category1.CategoryId,
                        AddressId = address1.AddressId,
                        OrderDate = DateTime.UtcNow.AddDays(-15),
                        TotalAmount = 749.99M,
                        Products = new List<Product> { product1  },
                        Tracking = new Tracking
                        {
                            OrderId = 1,
                            UserId = user1Id,
                            TrackingStatus = TrackingStatus.Pending,
                            UpdatedAt = DateTime.UtcNow.AddDays(-3)
                        }
                    },
                    new Order
                    {
                        UserId = user2Id,
                        CategoryId = category2.CategoryId,
                        AddressId = address2.AddressId,
                        OrderDate = DateTime.UtcNow.AddDays(-5),
                        TotalAmount = 89.99M,
                        Products = new List<Product> { product2 },
                        Tracking = new Tracking
                        {
                            OrderId = 2,
                            UserId = user2Id,
                            TrackingStatus = TrackingStatus.Delivered,
                            UpdatedAt = DateTime.UtcNow
                        }
                    }
                };
                context.Orders.AddRange(orders);
                await context.SaveChangesAsync();

            }

            // Conversations and Messages
            if (!context.Conversations.Any())
            {
                var conversations = new List<Conversation>
                {
                    new Conversation
                    {
                        BuyerId = user2Id,
                        SellerId = user3Id,
                        CreatedAt = DateTime.UtcNow.AddDays(-10),
                        LastMessageAt = DateTime.UtcNow.AddDays(-1),
                        Messages = new List<Message>
                        {
                            new Message
                            {
                                ConversationId = 1,
                                SenderId = user2Id,
                                Content = "Hello, I'm interested in your product.",
                                SentAt = DateTime.UtcNow.AddDays(-10)
                            },
                            new Message
                            {
                                ConversationId = 1,
                                SenderId = user3Id,
                                Content = "Great! Is there anything you want to know about the product",
                                SentAt = DateTime.UtcNow.AddDays(-9)
                            },
                            new Message {ConversationId = 1,
                                SenderId = user2Id,
                                Content = "Is there a discount available for the product?",
                                SentAt = DateTime.UtcNow.AddDays(-5)
                            },
                            new Message
                            {
                                ConversationId = 1,
                                SenderId = user3Id,
                                Content = "Yes, there is a 20% discount just for today.",
                                SentAt = DateTime.UtcNow.AddDays(-5)
                            },
                            new Message
                            {
                                ConversationId = 1,
                                SenderId = user2Id,
                                Content = "Great! Let I will make an order right away.",
                                SentAt = DateTime.UtcNow.AddDays(-9)
                            },
                            new Message
                            {
                                ConversationId = 1,
                                SenderId = user3Id,
                                Content = "Thank you for buying from me",
                                SentAt = DateTime.UtcNow.AddDays(-5)
                            }
                        }
                    }
                };
                context.Conversations.AddRange(conversations);
                await context.SaveChangesAsync();

            }

            // Notifications
            if (!context.Notifications.Any())
            {
                var notifications = new List<Notification>
                {
                    new Notification
                    {
                        OrderId = 1,
                        UserId = user1Id,
                        Message = "Your order has been shipped.",
                        Status = NotificationStatus.Sent,
                        NotificationType = NotificationType.ShippingUpdate,
                        CreatedAt = DateTime.UtcNow.AddDays(-2), IsSeller = false
                    },
                    new Notification
                    {
                        OrderId = 2,
                        UserId = user2Id,
                        Message = "Your order has been delivered.",
                        Status = NotificationStatus.Delivered,
                        NotificationType = NotificationType.DeliveryConfirmation,
                        CreatedAt = DateTime.UtcNow.AddDays(-1), IsSeller = false
                    }
                };
                context.Notifications.AddRange(notifications);
                await context.SaveChangesAsync();

            }

            // Tickets
            if (!context.Tickets.Any())
            {
                var tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        UserId = user1Id,
                        TicketCategory = TicketCategory.OrderIssues,
                        Title = "Issue with product",
                        Description = "The product I received is defective.",
                        TicketStatus = TicketStatus.Open,
                        CreatedAt = DateTime.UtcNow.AddDays(-7)
                    },
                    new Ticket
                    {
                        UserId = user3Id,
                        TicketCategory = TicketCategory.TechnicalSupport,
                        Title = "Product listing issue",
                        Description = "My product listing is not showing up correctly.",
                        TicketStatus = TicketStatus.Closed,
                        CreatedAt = DateTime.UtcNow.AddDays(-20),
                        ClosedAt = DateTime.UtcNow.AddDays(-10)
                    }
                };
                      context.Tickets.AddRange(tickets);
                await context.SaveChangesAsync();
            }

            // Save all changes to the database
        }
    }

}
