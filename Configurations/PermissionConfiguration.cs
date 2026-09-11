using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Constants;
using WebApi.Models;

namespace WebApi.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(p => p.Id);
        builder.HasData(
    new Permission
    {
        Id = 1,
        Name = Permissions.UsersRead
    },
    new Permission
    {
        Id = 2,
        Name = Permissions.UsersCreate
    }
);
    }
}