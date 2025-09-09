using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.Notes;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations;

internal sealed class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("notes");

        builder
            .HasKey(n => n.Id);

        builder
            .Property(n => n.Id)
            .HasConversion(id => id.Value, value => new NoteId(value))
            .ValueGeneratedNever();

        builder
            .Property(n => n.AuthorId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder
            .Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder
            .Property(n => n.Content)
            .IsRequired()
            .HasMaxLength(10000);

        builder
            .Property(n => n.CreatedAt)
            .IsRequired();

        builder
            .Property(n => n.UpdatedAt)
            .IsRequired();
    }
}
