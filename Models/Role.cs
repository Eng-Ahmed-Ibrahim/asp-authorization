using Microsoft.AspNetCore.Identity;

namespace WebApi.Models;
public class Role : IdentityRole<int>
{

    public ICollection<RolePermission> RolePermissions {get;set;} = new List<RolePermission>();
}