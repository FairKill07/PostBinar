using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Authorization;
using PostBinar.Domain.Enums;

namespace PostBinar.Persistence.Configurations.Authentication;

public sealed class RolePermissionConfiguration(AuthorizationOptions options) : IEntityTypeConfiguration<RolePermissionsEntity>
{
    private readonly AuthorizationOptions _options = options;

    public void Configure(EntityTypeBuilder<RolePermissionsEntity> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        builder.HasData(ParseRolePermission());
    }

    private RolePermissionsEntity[] ParseRolePermission()
    {
        return _options.RolePermission
            .SelectMany(rp => rp.Permissions
            .Select(p => new RolePermissionsEntity
            {
                RoleId = (int)Enum.Parse<Role>(rp.Role),
                PermissionId = (int)Enum.Parse<Permission>(p)
            }))
            .ToArray();
    }

}
