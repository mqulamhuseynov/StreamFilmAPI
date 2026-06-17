using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(d => d.Description).IsRequired().HasColumnType("varchar(max)");
            builder.Property(d => d.IconName).HasMaxLength(50).IsUnicode(false);
        }
    }
}
