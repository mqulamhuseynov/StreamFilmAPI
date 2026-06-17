using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(300).IsUnicode(false);

            
            builder.Property(c => c.Description).IsRequired().HasColumnType("varchar(max)");

            builder.Property(c => c.PosterUrl).IsRequired().HasMaxLength(500).IsUnicode(false);
            builder.Property(c => c.BackgroundUrl).IsRequired().HasMaxLength(500).IsUnicode(false);
            builder.Property(c => c.TrailerUrl).HasMaxLength(500).IsUnicode(false);

            builder.Property(c => c.Type).HasConversion<string>().IsUnicode(false).HasMaxLength(20);

            
            builder.Property(c => c.ImdbRating).HasPrecision(3, 1);
            builder.Property(c => c.StreamvibeRating).HasPrecision(3, 1);

            
            builder.Property(c => c.IsTrending).HasDefaultValue(false);
            builder.Property(c => c.IsNewRelease).HasDefaultValue(false);
            builder.Property(c => c.IsMustWatch).HasDefaultValue(false);
            builder.Property(c => c.IsFeatured).HasDefaultValue(false);

            
            builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("datetime2");
        }
    }
}
