using System;
using System.Linq;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public class SeedData
    {
        public static void Initialize(ConikeShopContext context)
        {
           context.Database.EnsureCreated(); 
                // Look for any movies.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        Title = "Baseball Bat",
                        Genre = "Baseball",
                        Price = 21.99M
                    },
                    new Product
                    {
                        Title = "Basketball ball",
                        Genre = "Basketball",
                        Price = 3.49M
                    },new Product
                    {
                        Title = "Football ball",
                        Genre = "Football",
                        Price = 7.99M
                    },new Product
                    {
                        Title = "Volleyball Ball",
                        Genre = "Volleyball",
                        Price = 4.99M
                    },new Product
                    {
                        Title = "Golf Club",
                        Genre = "Golf",
                        Price = 30.99M
                    }
                    
                );
                context.SaveChanges();
            
        }
    }
}