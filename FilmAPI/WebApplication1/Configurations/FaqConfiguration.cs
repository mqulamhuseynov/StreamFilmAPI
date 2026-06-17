using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class FaqConfiguration : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Question).IsRequired().HasMaxLength(300).IsUnicode(false);
            builder.Property(f => f.Answer).IsRequired().HasColumnType("varchar(max)");
        }
    }
}
