
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Entities.Auth;
using WebApplication1.Repository.Implementations;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.Implementations;
using WebApplication1.Service.Interfaces;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //env load
            Env.Load();
            //db connection (mssql)
            var connectionString = Environment.GetEnvironmentVariable("DATABASE");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            var jwtSecret = Environment.GetEnvironmentVariable("jwtSecret");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
             {
             options.TokenValidationParameters = new TokenValidationParameters
              {
               ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSecret!)),
            ValidateIssuer = false,
             ValidateAudience = false,
               ClockSkew = TimeSpan.Zero  
           };
});

            //services,repos di
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IFaqRepository, FaqRepository>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
            builder.Services.AddScoped<IContentRepository, ContentRepository>();

            builder.Services.AddScoped<IContentService, ContentService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IFaqService, FaqService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();

            //cors
            var corsConnection = Environment.GetEnvironmentVariable("CORS");
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Front",
                                      policy =>
                                      {
                                          policy.WithOrigins(corsConnection!)
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod();
                                      });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("Front");

            app.MapControllers();

            app.Run();
        }
    }
}
