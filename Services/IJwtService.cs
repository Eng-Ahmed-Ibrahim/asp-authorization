namespace WebApi.Services;
public interface IJwtService
{
    string GenerateToken(int userId, string email, List<string> roles,List<string> perimssions);
}