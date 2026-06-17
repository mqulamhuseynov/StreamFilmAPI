using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.ReviewerName).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(r => r.ReviewerLocation).HasMaxLength(100).IsUnicode(false);
            builder.Property(r => r.Rating).IsRequired().HasPrecision(3, 1);
            builder.Property(r => r.ReviewText).IsRequired().HasColumnType("varchar(max)");
            builder.Property(r => r.CreatedAt).IsRequired().HasColumnType("datetime2");

            builder.HasOne(r => r.Content)
                   .WithMany(c => c.Reviews)
                   .HasForeignKey(r => r.ContentId);
        }
    }
}
