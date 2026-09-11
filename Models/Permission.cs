namespace WebApi.Models;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<RolePermission> RolePermission { get; set; } = new List<RolePermission>();


}