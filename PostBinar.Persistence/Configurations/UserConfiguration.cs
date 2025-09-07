using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder
            .HasKey(u => u.Id);
        
        builder
            .Property(u => u.Id)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.ProfilePhoto)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder
            .Property(u => u.CreatedAt)
            .IsRequired();
        
        builder
            .Property(u => u.UpdatedAt)
            .IsRequired(false);
        
        builder
            .Property(u => u.TgChatId)
            .HasMaxLength(100)
            .IsRequired(false);


        builder.HasIndex(u => u.Email).IsUnique();

        builder
            .Property(u => u.SpecializationId);

        builder
            .HasOne(u => u.Specialization)
            .WithMany()
            .HasForeignKey(u => u.SpecializationId)
            .IsRequired();

        builder
            .HasMany(u => u.ProjectMemberships)
            .WithOne(pm => pm.User)
            .HasForeignKey(pm => pm.UserId)
            .IsRequired();
    }
}
