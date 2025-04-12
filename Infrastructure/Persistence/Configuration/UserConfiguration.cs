using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(u => u.Posts)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId);

            builder.HasQueryFilter(u => u.DeletedAt == null);
        }
    }
}
