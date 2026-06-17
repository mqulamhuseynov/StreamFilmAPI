using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{

        public class GenreConfiguration : IEntityTypeConfiguration<Genre>
        {
            public void Configure(EntityTypeBuilder<Genre> builder)
            {
                builder.HasKey(g => g.Id);

                //IsUnicode varchar
                builder.Property(g => g.Name).IsRequired().HasMaxLength(50).IsUnicode(false);
                builder.Property(g => g.Slug).IsRequired().HasMaxLength(50).IsUnicode(false);
                builder.HasIndex(g => g.Slug).IsUnique();

                builder.Property(g => g.Type).HasConversion<string>().IsUnicode(false).HasMaxLength(20);

                builder.Property(g => g.CoverImage1).HasMaxLength(500).IsUnicode(false);
                builder.Property(g => g.CoverImage2).HasMaxLength(500).IsUnicode(false);
                builder.Property(g => g.CoverImage3).HasMaxLength(500).IsUnicode(false);
                builder.Property(g => g.CoverImage4).HasMaxLength(500).IsUnicode(false);
            }
        }
}

