using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Orders;
using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Infrastructure.Persistence._Common;
using System.Text.Json;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase
{
    public class StoreDbInitializer(StoreDbContext dbContext) : DbInitializer(dbContext), IStoreDbInitializer
    {
        public override async Task SeedAsync()
        {
            if (!dbContext.Brands.Any())
            {
                var Data = await File.ReadAllTextAsync("../Karim.ECommerce.Infrastructure.Persistence/_StoreDatabase/Seeds/Brand.Json");
                if (Data is not null)
                {
                    var SerializedData = JsonSerializer.Deserialize<List<Brand>>(Data);
                    await dbContext.AddRangeAsync(SerializedData!);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Categories.Any())
            {
                var Data = await File.ReadAllTextAsync("../Karim.ECommerce.Infrastructure.Persistence/_StoreDatabase/Seeds/Category.Json");
                if (Data is not null)
                {
                    var SerializedData = JsonSerializer.Deserialize<List<Category>>(Data);
                    await dbContext.AddRangeAsync(SerializedData!);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.CategoryBrand.Any())
            {
                var Data = await File.ReadAllTextAsync("../Karim.ECommerce.Infrastructure.Persistence/_StoreDatabase/Seeds/BrandCategory.Json");
                if (Data is not null)
                {
                    var SerializedData = JsonSerializer.Deserialize<List<CategoryBrand>>(Data);
                    await dbContext.AddRangeAsync(SerializedData!);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {
                var Data = await File.ReadAllTextAsync("../Karim.ECommerce.Infrastructure.Persistence/_StoreDatabase/Seeds/Product.Json");
                if (Data is not null)
                {
                    var SerializedData = JsonSerializer.Deserialize<List<Product>>(Data);
                    await dbContext.AddRangeAsync(SerializedData!);
                    await dbContext.SaveChangesAsync();
                }
            }

            if(!dbContext.DeliveryMethods.Any())
            {
                var Data = await File.ReadAllTextAsync("../Karim.ECommerce.Infrastructure.Persistence/_StoreDatabase/Seeds/DeliveryMethod.Json");
                if (Data is not null)
                {
                    var SerializedData = JsonSerializer.Deserialize<List<DeliveryMethod>>(Data);
                    await dbContext.AddRangeAsync(SerializedData!);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
