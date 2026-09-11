using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;
namespace WebApi.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;

    public JwtService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public  string GenerateToken(int userId, string email, List<string> roles, List<string> permissions )
    {
        var jwtSettings = configuration.GetSection("JWT");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                jwtSettings["SecretKey"]!
            )
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.ToString()
            ),

            new Claim(
                ClaimTypes.Email,
                email
            ),
            
        };


        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        foreach (var permission in permissions)
        {   
            claims.Add(new Claim("permission",permission));
        }
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }


}