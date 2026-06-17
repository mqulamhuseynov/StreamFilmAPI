using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class ContentPersonConfiguration : IEntityTypeConfiguration<ContentPerson>
    {
        public void Configure(EntityTypeBuilder<ContentPerson> builder)
        {
            builder.HasKey(cp => cp.Id);
            builder.Property(cp => cp.RoleType).HasConversion<string>().IsUnicode(false).HasMaxLength(20);
            builder.Property(cp => cp.CharacterName).HasMaxLength(100).IsUnicode(false);

            builder.HasOne(cp => cp.Content)
                   .WithMany(c => c.ContentPeople)
                   .HasForeignKey(cp => cp.ContentId);

            builder.HasOne(cp => cp.Person)
                   .WithMany(p => p.ContentPeople)
                   .HasForeignKey(cp => cp.PersonId);
        }
    }
}
