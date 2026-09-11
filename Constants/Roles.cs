namespace WebApi.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Manager = "Manager";
    public const string User = "User";
    public static readonly List<string> AllRoles = new List<string> { Admin, User,Manager };
}