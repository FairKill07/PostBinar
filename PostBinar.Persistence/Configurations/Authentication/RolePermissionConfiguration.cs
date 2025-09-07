using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Authorization;
using PostBinar.Domain.Enums;

namespace PostBinar.Persistence.Configurations.Authentication;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionsEntity>
{
    private readonly AuthorizationOptions _options;
    public RolePermissionConfiguration(AuthorizationOptions options)
    {
        _options = options;
    }

    public void Configure(EntityTypeBuilder<RolePermissionsEntity> builder)
    {
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
