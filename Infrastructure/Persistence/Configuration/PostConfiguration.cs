using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasMany(p => p.Comments)
                   .WithOne(c => c.Post)
                   .HasForeignKey(c => c.PostId);

            builder.HasQueryFilter(p => p.DeletedAt == null);
        }
    }
}
