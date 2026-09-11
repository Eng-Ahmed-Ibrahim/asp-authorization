using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Constants;
using WebApi.Data;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IJwtService _jwtService, UserManager<ApplicationUser> _userManager, AppDbContext dbContext) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        Console.WriteLine($"User found: {user?.Email}");
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }
        var roles = (await _userManager.GetRolesAsync(user)).ToList();
        Console.WriteLine($"User roles: {string.Join(", ", roles)}");
        var permissions = await dbContext.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Name)
            .Distinct()
            .ToListAsync();
        Console.WriteLine($"Permissions: {string.Join(", ", permissions)}");
        var token = _jwtService.GenerateToken(user.Id, user.Email, roles,permissions);
        Console.WriteLine($"Token: {token}");
        return Ok(new { message = "Login successful", token });

    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                message = "Failed to create user",
                errors = result.Errors.Select(e => new
                {
                    code = e.Code,
                    description = e.Description
                })
            });
        }

        var addRoleToUser = await _userManager.AddToRoleAsync(user, Roles.Admin);
        var roles = await _userManager.GetRolesAsync(user);

        var permissions = await dbContext.UserRoles
            .Where(ur=>ur.UserId == user.Id)
            .SelectMany(ur=>ur.Role.RolePermissions)
            .Select(rp=>rp.Permission.Name)
            .Distinct()
            .ToListAsync();
        var CreateToken = _jwtService.GenerateToken(user.Id, user.Email, roles.ToList(),permissions);
        Console.WriteLine($"Token: {CreateToken}");
        return Ok(new { message = "User created successfully", token = CreateToken });
    }
}