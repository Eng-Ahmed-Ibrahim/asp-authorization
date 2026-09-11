using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Authorization;

public class PermissionAuthorizationHandler(AppDbContext _context)
 : AuthorizationHandler<PermissionRequirment>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
    {
        if (context.User.Identity?.IsAuthenticated != true)
        {
            return;
        }

        var userId = context.User
            .FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;
        if (userId is null)
            return;
        var hasPermission = context.User
        .Claims
        .Any(c=>c.Type=="permission" && c.Value == requirement.Permission );
        if (hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}