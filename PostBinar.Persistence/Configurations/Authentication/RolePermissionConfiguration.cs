using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Authorization;
using PostBinar.Domain.Enums;
using PostBinar.Infrastructure.Authorization;

namespace PostBinar.Persistence.Configurations.Authentication;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissions>
{
    private readonly AuthorizationOptions _options;
    public RolePermissionConfiguration(AuthorizationOptions options)
    {
        _options = options;
    }

    public void Configure(EntityTypeBuilder<RolePermissions> builder)
    {
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        builder.HasData(ParseRolePermission());
    }

    private RolePermissions[] ParseRolePermission()
    {
        return _options.RolePermission
            .SelectMany(rp => rp.Permissions
            .Select(p => new RolePermissions
            {
                RoleId = (int)Enum.Parse<Role>(rp.Role),
                PermissionId = (int)Enum.Parse<Permission>(p)
            }))
            .ToArray();
    }

}
