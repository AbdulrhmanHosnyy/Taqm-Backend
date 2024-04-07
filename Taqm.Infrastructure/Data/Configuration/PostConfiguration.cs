using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taqm.Data.Entities;

namespace Taqm.Infrastructure.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.PostId);
            builder.Property(p => p.ProductDescription).HasMaxLength(500);
            builder.Property(p => p.ProductCategory).HasMaxLength(50);
            builder.Property(p => p.ProductGender).HasMaxLength(50);
            builder.Property(p => p.ProductSeason).HasMaxLength(50);
            builder.Property(p => p.ProductColor).HasMaxLength(50);
            builder.Property(p => p.ProductCondition).HasMaxLength(50);
            builder.Property(p => p.ProductSize).HasMaxLength(50);
        }
    }
}
