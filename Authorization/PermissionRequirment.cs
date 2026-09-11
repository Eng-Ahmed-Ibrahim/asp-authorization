using Microsoft.AspNetCore.Authorization;

namespace WebApi.Authorization;
public class PermissionRequirment : IAuthorizationRequirement
{
    public string Permission {get;set;}
    public PermissionRequirment(string permission)
    {
        Permission=permission;
    }
}