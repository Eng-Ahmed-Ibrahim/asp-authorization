using WebApi.Models;

namespace WebApi.Data.Seeder;

public class ProductsSeeder(AppDbContext context) : ISeeder
{
    public async Task SeedAsync()
    {
        if(context.Products.Any())
            return ;
        var listProducts = new List<Product>();
        for(int i = 1; i <= 100; i++)
        {
            listProducts.Add(new Product
            {
                Name=$"Product {i}"
            });
        }
        await context.AddRangeAsync(listProducts);
        await context.SaveChangesAsync();

    }
}