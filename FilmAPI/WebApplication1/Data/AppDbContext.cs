using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Entities.Auth;

namespace WebApplication1.Data
{
    public class AppDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentGenre> ContentGenres { get; set; }
        public DbSet<ContentLanguage> ContentLanguages { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<ContentPerson> ContentPeople { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<PricingPlan> PricingPlans { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //konfiqleri avtomatik tapir
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
