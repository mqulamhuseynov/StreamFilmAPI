using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Entities;

namespace WebApplication1.Configurations
{
    public class PricingPlanConfiguration : IEntityTypeConfiguration<PricingPlan>
    {
        public void Configure(EntityTypeBuilder<PricingPlan> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(max)");
            builder.Property(p => p.PriceMonthly).IsRequired().HasPrecision(6, 2);
            builder.Property(p => p.PriceYearly).IsRequired().HasPrecision(6, 2);
            builder.Property(p => p.IsPopular).HasDefaultValue(false);
        }
    }
}
