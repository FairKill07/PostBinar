using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Categorys;

namespace PostBinar.Persistence.Configurations;

internal sealed class NoteCategoryConfiguration : IEntityTypeConfiguration<NoteCategory>
{
    public void Configure(EntityTypeBuilder<NoteCategory> builder)
    {
        builder.ToTable("note_categories");
        
        builder
            .HasKey(nc => nc.Id);
        
        builder
            .Property(nc => nc.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(nc => nc.ColorCode)
            .IsRequired()
            .HasMaxLength(7);
    }
}
