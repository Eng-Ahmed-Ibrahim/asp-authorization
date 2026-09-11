using Microsoft.AspNetCore.Identity;
using WebApi.Constants;
using WebApi.Models;

namespace WebApi.Data.Seeder;

public class RolesSeeder : ISeeder
{
private readonly RoleManager<Role> roleManager;
    public RolesSeeder(RoleManager<Role> _roleManager)
    {
        roleManager = _roleManager;
    }
    public async Task SeedAsync()
    {
        foreach (var roleName in Roles.AllRoles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var newRole = new Role
                {
                    Name = roleName
                };

                var result = await roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    throw new Exception(
                        string.Join(
                            ", ",
                            result.Errors.Select(e => e.Description)
                        )
                    );
                }
            }
        }
    }
}