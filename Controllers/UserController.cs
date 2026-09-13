using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Authorization;
using WebApi.Constants;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController(UserManager<ApplicationUser> _userManager, AppDbContext _context,IHttpContextAccessor httpContextAccessor) : ControllerBase
{

    [HttpGet("all")]
    // [Authorize(Roles = Roles.Admin)]
    [HasPermission(Permissions.UsersRead)]
    public async Task<IActionResult> GetAllUsers(
   
    )
    {

        var users = await _userManager.Users.ToListAsync();
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine($"UserId { userId}");
        var userDtos = users.Select(user => new
        {
            user.Id,
            user.UserName,
            user.Email,
            user.FirstName,
            user.LastName
        }).ToList();

        return Ok(userDtos);
    }
    [HttpGet("debug")]
public IActionResult Debug()
{
    return Ok(new
    {
        IsAuthenticated = User.Identity?.IsAuthenticated,
        AuthenticationType = User.Identity?.AuthenticationType,
        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
        Email = User.FindFirst(ClaimTypes.Email)?.Value,
        Claims = User.Claims.Select(c => new
        {
            c.Type,
            c.Value
        })
    });
}
}