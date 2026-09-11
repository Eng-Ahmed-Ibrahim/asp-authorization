using Microsoft.AspNetCore.Identity;
using WebApi.Constants;
using WebApi.Data.Seeder;
using WebApi.Models;

namespace WebApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var seeders = services.GetRequiredService<IEnumerable<ISeeder>>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }
}