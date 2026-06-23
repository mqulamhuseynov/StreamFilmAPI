
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
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

            //services,repos di
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IFaqRepository, FaqRepository>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

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

            app.UseAuthorization();

            app.UseCors("Front");

            app.MapControllers();

            app.Run();
        }
    }
}
