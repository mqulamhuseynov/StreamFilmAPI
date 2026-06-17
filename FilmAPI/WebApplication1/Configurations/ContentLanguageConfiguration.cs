using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class ContentLanguageConfiguration : IEntityTypeConfiguration<ContentLanguage>
    {
        public void Configure(EntityTypeBuilder<ContentLanguage> builder)
        {
            builder.HasKey(cl => cl.Id);
            builder.Property(cl => cl.Language).IsRequired().HasMaxLength(50).IsUnicode(false);

            builder.HasOne(cl => cl.Content)
                   .WithMany(c => c.ContentLanguages)
                   .HasForeignKey(cl => cl.ContentId);
        }
    }
}
