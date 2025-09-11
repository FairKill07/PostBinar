using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Authorization;
using PostBinar.Domain.Enums;

namespace PostBinar.Persistence.Configurations.Authentication;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Id);
        
        builder
            .Property(p => p.Name)
            .HasMaxLength(100);
       
        var permissions = Enum
            .GetValues<Permission>()
            .Select(p => new PermissionEntity
            {
                Id = (int)p,
                Name = p.ToString()
            });

        builder.HasData(permissions);
    }
}
