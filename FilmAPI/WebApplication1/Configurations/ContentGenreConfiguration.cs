using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class ContentGenreConfiguration : IEntityTypeConfiguration<ContentGenre>
    {
        public void Configure(EntityTypeBuilder<ContentGenre> builder)
        {
            builder.HasKey(cg => cg.Id);

            builder.HasOne(cg => cg.Content)
                   .WithMany(c => c.ContentGenres)
                   .HasForeignKey(cg => cg.ContentId);

            builder.HasOne(cg => cg.Genre)
                   .WithMany(g => g.ContentGenres)
                   .HasForeignKey(cg => cg.GenreId);
        }
    }
}
