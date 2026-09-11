using Microsoft.AspNetCore.Identity;

namespace WebApi.Models;
public class UserRole : IdentityUserRole<int>
{
    public ApplicationUser User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}