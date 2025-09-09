using Microsoft.EntityFrameworkCore;
using PostBinar.Domain.Comments;
using PostBinar.Domain.Projects;
using PostBinar.Domain.Users;

namespace PostBinar.Persistence.Configurations
{
    internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasConversion(id => id.Value, value => new CommentId(value))
                .ValueGeneratedNever();

            builder
                .Property(c => c.AuthorId)
                .HasConversion(id => id.Value, value => new UserId(value))
                .IsRequired();

            builder
                .Property(c => c.ProjectId)
                .HasConversion(id => id.Value, value => new ProjectId(value))
                .IsRequired();

            builder
                .Property(c => c.ObjectId)
                .IsRequired();

            builder
                .Property(c => c.ObjectType)
                .IsRequired()
                .HasConversion<string>();

            builder
                .Property(c => c.Context)
                .IsRequired();

            builder
                .Property(c => c.CreatedAt)
                .IsRequired();

            builder
                .Property(c => c.UpdatedAt)
                .IsRequired();

            builder
                .Property(c => c.IsActive)
                .IsRequired();

            builder
              .HasOne<User>()
              .WithMany()
              .HasForeignKey(c => c.AuthorId)
              .OnDelete(DeleteBehavior.Restrict); 

            builder
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
