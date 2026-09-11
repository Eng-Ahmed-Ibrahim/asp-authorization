using Microsoft.AspNetCore.Identity;
using WebApi.Constants;
using WebApi.Models;

namespace WebApi.Data.Seeder;

public class UserSeeder(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager) : ISeeder
{

    public async Task SeedAsync()
    {


        foreach (var _user in Users.users)
        {

            var user = await userManager.FindByNameAsync(_user?.UserName);

            if (user == null)
            {
                user = _user;
                var userResult =
                    await userManager.CreateAsync(user, "Ahmed@123");

                if (!userResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(
                            ", ",
                            userResult.Errors.Select(e => e.Description)
                        )
                    );
                }
            }
            if (!await userManager.IsInRoleAsync(user, Roles.Admin))
            {
                var roleResult =
                    await userManager.AddToRoleAsync(user, Roles.Admin);

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(
                            ", ",
                            roleResult.Errors.Select(e => e.Description)
                        )
                    );
                }
            }
        }

    }

}