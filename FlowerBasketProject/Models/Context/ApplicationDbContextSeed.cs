using Microsoft.AspNetCore.Identity;
using FlowerBasketProject.Models.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace FlowerBasketProject.Models.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure the database is created.
            context.Database.EnsureCreated();

            // Seed Roles
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                FullName = "Admin User"
            };

            string adminPassword = "Admin@123";
            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Flowers" },
                    new Category { Name = "Plants" },
                    new Category { Name = "Gifts" }
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var flowersCategory = context.Categories.FirstOrDefault(c => c.Name == "Flowers");
                var plantsCategory = context.Categories.FirstOrDefault(c => c.Name == "Plants");

                var products = new List<Product>
       {
           new Product { Name = "Roses", Description = "A bouquet of red roses", Price = 50.0m, CategoryId = flowersCategory.Id, IsNew = true },
           new Product { Name = "Tulips", Description = "A bouquet of tulips", Price = 30.0m, CategoryId = flowersCategory.Id, IsNew = false },
           new Product { Name = "Ficus", Description = "A beautiful ficus plant", Price = 40.0m, CategoryId = plantsCategory.Id, IsNew = true },
           new Product { Name = "Sunflowers", Description = "Bright yellow sunflowers", Price = 25.0m, CategoryId = flowersCategory.Id, IsNew = true },
           new Product { Name = "Orchids", Description = "Elegant orchid flowers", Price = 60.0m, CategoryId = flowersCategory.Id, IsNew = false },
           new Product { Name = "Cactus", Description = "Small decorative cactus", Price = 20.0m, CategoryId = plantsCategory.Id, IsNew = false },
           new Product { Name = "Lily", Description = "Beautiful white lilies", Price = 35.0m, CategoryId = flowersCategory.Id, IsNew = true },
           new Product { Name = "Bonsai", Description = "Miniature bonsai tree", Price = 100.0m, CategoryId = plantsCategory.Id, IsNew = true }
       };
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            // Seed Images
            if (!context.Images.Any())
            {
                var rosesProduct = context.Products.FirstOrDefault(p => p.Name == "Roses");
                var tulipsProduct = context.Products.FirstOrDefault(p => p.Name == "Tulips");
                var ficusProduct = context.Products.FirstOrDefault(p => p.Name == "Ficus");
                var sunflowersProduct = context.Products.FirstOrDefault(p => p.Name == "Sunflowers");
                var orchidsProduct = context.Products.FirstOrDefault(p => p.Name == "Orchids");
                var cactusProduct = context.Products.FirstOrDefault(p => p.Name == "Cactus");
                var lilyProduct = context.Products.FirstOrDefault(p => p.Name == "Lily");
                var bonsaiProduct = context.Products.FirstOrDefault(p => p.Name == "Bonsai");

                var images = new List<Image>
       {
           new Image { Url = "/Images/1.webp", ProductId = rosesProduct.Id },
           new Image { Url = "/Images/2.webp", ProductId = tulipsProduct.Id },
           new Image { Url = "/Images/3.webp", ProductId = ficusProduct.Id },
           new Image { Url = "/Images/4.webp", ProductId = sunflowersProduct.Id },
           new Image { Url = "/Images/5.webp", ProductId = orchidsProduct.Id },
           new Image { Url = "/Images/6.webp", ProductId = cactusProduct.Id },
           new Image { Url = "/Images/7.webp", ProductId = lilyProduct.Id },
           new Image { Url = "/Images/8.webp", ProductId = bonsaiProduct.Id }
       };
                context.Images.AddRange(images);
                await context.SaveChangesAsync();
            }

            // Seed Orders
            if (!context.Orders.Any())
            {
                var customer = await userManager.FindByEmailAsync("customer@customer.com");
                if (customer == null)
                {
                    customer = new ApplicationUser
                    {
                        UserName = "customer@customer.com",
                        Email = "customer@customer.com",
                        FullName = "Customer User"
                    };
                    await userManager.CreateAsync(customer, "Customer@123");
                    await userManager.AddToRoleAsync(customer, "User");
                }

                var orders = new List<Order>
                {
                    new Order { OrderDate = DateTime.Now, UserId = customer.Id }
                };
                context.Orders.AddRange(orders);
                await context.SaveChangesAsync();
            }

            // Seed OrderDetails
            if (!context.OrderDetails.Any())
            {
                var order = context.Orders.FirstOrDefault();
                var rosesProduct = context.Products.FirstOrDefault(p => p.Name == "Roses");

                var orderDetails = new List<OrderDetail>
                {
                    new OrderDetail { OrderId = order.Id, ProductId = rosesProduct.Id, Quantity = 1 }
                };
                context.OrderDetails.AddRange(orderDetails);
                await context.SaveChangesAsync();
            }

            // Seed Notifications
            if (!context.Notifications.Any())
            {
                var notifications = new List<Notification>
                {
                    new Notification { Title = "Welcome!", Message = "Welcome to the Flower Basket!", Timestamp = DateTime.Now, IsRead = false, UserId = adminUser.Id },
                    new Notification { Title = "Order Shipped", Message = "Your order has been shipped!", Timestamp = DateTime.Now, IsRead = false, UserId = adminUser.Id }
                };
                context.Notifications.AddRange(notifications);
                await context.SaveChangesAsync();
            }

            // Seed Messages
            if (!context.Messages.Any())
            {
                var messages = new List<Message>
                {
                    new Message { SenderName = "Admin", Content = "Welcome to the Flower Basket!", Timestamp = DateTime.Now, IsRead = false, UserId = adminUser.Id },
                    new Message { SenderName = "Support", Content = "Your support request has been received.", Timestamp = DateTime.Now, IsRead = false, UserId = adminUser.Id }
                };
                context.Messages.AddRange(messages);
                await context.SaveChangesAsync();
            }
        }
    }
}