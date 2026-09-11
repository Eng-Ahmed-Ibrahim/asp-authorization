using Microsoft.AspNetCore.Authorization;
using WebApi.Models;

namespace WebApi.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: permission.ToString())
    {
    }
}