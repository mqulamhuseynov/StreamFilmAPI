using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(p => p.AvatarUrl).HasMaxLength(500).IsUnicode(false);
            builder.Property(p => p.Nationality).HasMaxLength(100).IsUnicode(false);
        }
    }
}
