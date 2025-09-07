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
        }
    }
}
