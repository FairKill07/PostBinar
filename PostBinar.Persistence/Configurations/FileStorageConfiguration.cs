using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;

namespace PostBinar.Persistence.Configurations;

internal sealed class FileStorageConfiguration : IEntityTypeConfiguration<FileStorage>
{
    public void Configure(EntityTypeBuilder<FileStorage> builder)
    {
        builder.ToTable("file_storages");
        
        builder
            .HasKey(fs => fs.Id);
        
        builder
            .Property(fs => fs.Id)
            .HasConversion(id => id.Value, value => new FileStorageId(value))
            .ValueGeneratedNever();

        builder
            .Property(fs => fs.ProjectId)
            .HasConversion(id => id.Value, value => new ProjectId(value))
            .IsRequired();

        builder
            .Property(fs => fs.ObjectType)
            .HasConversion<string>()
            .IsRequired();

        builder
            .Property(fs => fs.FileName)
            .IsRequired();

        builder
            .Property(fs => fs.StorageKey)
            .IsRequired();

        builder
            .Property(fs => fs.MimeType)
            .IsRequired();

        builder
            .Property(fs => fs.Size)
            .IsRequired();

        builder
            .Property(fs => fs.CreatedAt)
            .IsRequired();
        
        builder
            .Property(fs => fs.UpdatedAt);

        builder
            .Property(fs => fs.IsActive)
            .IsRequired();
    }
}
