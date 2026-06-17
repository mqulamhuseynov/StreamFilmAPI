using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200).IsUnicode(false);
            builder.Property(e => e.Description).HasColumnType("varchar(max)");
            builder.Property(e => e.ThumbnailUrl).HasMaxLength(500).IsUnicode(false);

            builder.HasOne(e => e.Season)
                   .WithMany(s => s.Episodes)
                   .HasForeignKey(e => e.SeasonId);
        }
    }
}
