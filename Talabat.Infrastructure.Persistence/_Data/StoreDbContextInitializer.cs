using System.Text.Json;
using Talabat.Core.Domain.Contract.Persistence.DbContextInitializer;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Infrastructure.Persistence.Common;

namespace Talabat.Infrastructure.Persistence.Data
{
    internal class StoreDbContextInitializer(StoreDbContext dbContext) : DbContextInitializer(dbContext), IStoreDbContextInitializer
    {
        public override async Task SeedAsync()
        {
            if (!dbContext.Brands.Any())
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var jsonBrands = await File.ReadAllTextAsync($"../Talabat.Infrastructure.Persistence/_Data/DataSeeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(jsonBrands);

                if (brands?.Count > 0)
                    foreach (var brand in brands)
                    {
                        await dbContext.Set<ProductBrand>().AddRangeAsync(brand);
                    }
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Categories.Any())
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var jsonCategories = await File.ReadAllTextAsync($"../Talabat.Infrastructure.Persistence/_Data/DataSeeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(jsonCategories);

                if (categories?.Count > 0)
                    foreach (var category in categories)
                    {
                        await dbContext.Set<ProductCategory>().AddRangeAsync(category);
                    }
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Products.Any())
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var jsonProducts = await File.ReadAllTextAsync($"../Talabat.Infrastructure.Persistence/_Data/DataSeeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(jsonProducts);

                if (products?.Count > 0)
                    foreach (var product in products)
                    {
                        await dbContext.Set<Product>().AddRangeAsync(product);
                    }
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.DeliveryMethods.Any())
            {
                var jsonDeliveries = await File.ReadAllTextAsync($"../Talabat.Infrastructure.Persistence/_Data/DataSeeds/delivery.json");
                var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(jsonDeliveries);

                if (deliveries?.Count > 0)
                    foreach (var delivery in deliveries)
                    {
                        await dbContext.Set<DeliveryMethod>().AddRangeAsync(delivery);
                    }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
