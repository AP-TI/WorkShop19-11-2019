using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;
using WebShop.Data.Entities;

namespace WebShop.Data.Context
{
    public class WebShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        public WebShopContext() { } // voor migraties
        
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            Seed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // voor migraties
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=pw")
                ;
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            var categories = new[]
            {
                new Category
                {
                    Id = 1,
                    Description = "T-shirts"
                },
                new Category
                {
                    Id = 2,
                    Description = "Sweaters"
                }
            };

            var tshirts =
                Enumerable.Range(1, 10000)
                    .Select(i => new Product
                    {
                        Id = i,
                        CategoryId = 1,
                        Name = $"Shirt {i}",
                        Description = $"Shirt {i} desc",
                        AmountInStock = i * 100,
                        Price = i * 5
                    });

            var sweaters =
                Enumerable.Range(1, 5)
                .Select(i => new Product
                {
                    Id = 10000 + i,
                    CategoryId = 2,
                    Name = $"Sweater {i}",
                    Description = $"Sweater {i} desc",
                    AmountInStock = i * 105,
                    Price = i * 6
                });
            var promotions = new[]
            {
                new Promotion
                {
                    Id = 1,
                    CategoryId = 2,
                    DiscountPercentage = 10,
                    Name = "Black tuesday"
                }
            };
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(tshirts.Concat(sweaters));
            modelBuilder.Entity<Promotion>().HasData(promotions);
        }
    }
}
