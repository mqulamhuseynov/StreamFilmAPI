using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Title).HasMaxLength(100).IsUnicode(false);

            builder.HasOne(s => s.Content)
                   .WithMany(c => c.Seasons)
                   .HasForeignKey(s => s.ContentId);
        }
    }
}
