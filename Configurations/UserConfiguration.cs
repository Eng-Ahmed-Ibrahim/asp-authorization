using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Constants;
using WebApi.Models;

namespace WebApi.Configurations;

public class UserConfiguration(UserManager<ApplicationUser> userManager) : IEntityTypeConfiguration<ApplicationUser>
{
    public async void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("users");
        var user = new ApplicationUser
        {
            UserName = "Ahmed_ebrahim",
            Email = "ahmed@gmail.com",
            FirstName = "Ahmed",
            LastName = "Salem"
        };

        var result = await userManager.CreateAsync(user, "Admin123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, Roles.Admin);
        }
    }
}