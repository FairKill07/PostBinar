using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Categorys;

namespace PostBinar.Persistence.Configurations;

internal sealed class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder
            .ToTable("specializations");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(cc => cc.ColorCode)
            .HasMaxLength(7)
            .IsRequired();
    }
}
