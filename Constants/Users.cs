using WebApi.Models;

namespace WebApi.Constants;

public static class Users
{
    public static List<ApplicationUser> users = new List<ApplicationUser>
     {
        new ApplicationUser
        {

                UserName = "Ahmed_ebrahim",
                Email = "ahmed@gmail.com",
                FirstName = "Ahmed",
                LastName = "Salem",
        },
        new ApplicationUser
        {
                     UserName = "user",
                Email = "user@gmail.com",
                FirstName = "Ahmed",
                LastName = "Salem",
        }
    };
}