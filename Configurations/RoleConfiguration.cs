using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Constants;
using WebApi.Models;

namespace WebApi.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
        builder.HasMany(r => r.RolePermissions)
               .WithOne(rp => rp.Role)
               .HasForeignKey(rp => rp.RoleId);

  builder.HasData(
    new Role
    {
        Id = 1,
        Name = Roles.Admin,
        NormalizedName = Roles.Admin.ToUpperInvariant(),
        ConcurrencyStamp = "admin-role-stamp"
    },
    new Role
    {
        Id = 2,
        Name = Roles.Manager,
        NormalizedName = Roles.Manager.ToUpperInvariant(),
        ConcurrencyStamp = "manager-role-stamp"
    }
);
    }
}