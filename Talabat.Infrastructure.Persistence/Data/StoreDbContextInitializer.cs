using System.Text.Json;
using Talabat.Core.Domain.Contract;
using Talabat.Core.Domain.Entities.Product;

namespace Talabat.Infrastructure.Persistence.Data
{
    internal class StoreDbContextInitializer(StoreDbContext _dbContext) : IStoreDbContextInitializer
    {
        private readonly StoreDbContext _dbContext = _dbContext;

        public async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(); // Update database
        }

        public async Task SeedAsync()
        {
            if (!_dbContext.Brands.Any())
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var jsonBrands = await File.ReadAllTextAsync($"../Talabat.Infrastructure.Persistence/Data/DataSeeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(jsonBrands);

                if (brands?.Count > 0)
                    foreach (var brand in brands)
                    {
                        await _dbContext.Set<ProductBrand>().AddRangeAsync(brand);
                    }
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Categories.Any())
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var jsonCategories = await File.ReadAllTextAsync($"../Talabat.Infrastructure.Persistence/Data/DataSeeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(jsonCategories);

                if (categories?.Count > 0)
                    foreach (var category in categories)
                    {
                        await _dbContext.Set<ProductCategory>().AddRangeAsync(category);
                    }
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Products.Any())
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var jsonProducts = await File.ReadAllTextAsync($"../Talabat.Infrastructure.Persistence/Data/DataSeeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(jsonProducts);

                if (products?.Count > 0)
                    foreach (var product in products)
                    {
                        await _dbContext.Set<Product>().AddRangeAsync(product);
                    }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
