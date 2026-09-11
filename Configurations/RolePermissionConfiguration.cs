using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Constants;
using WebApi.Models;

public class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{

    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(r => new
        {
            r.RoleId,
            r.PermissionId
        });

        builder.HasOne(r => r.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(r => r.RoleId);

        builder.HasOne(r => r.Permission)
            .WithMany(p => p.RolePermission)
            .HasForeignKey(r => r.PermissionId);
        builder.HasData(
            new RolePermission
            {
                RoleId = 1,
                PermissionId = 1
            }
        );
    }

}