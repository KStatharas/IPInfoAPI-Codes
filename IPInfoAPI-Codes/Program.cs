using IP2C_IPInfoProvider.Services;
using IPInfoAPI_Codes.BackgroundServices;
using IPInfoAPI_Codes.Data;
using IPInfoAPI_Codes.Repositories;
using IPInfoAPI_Codes.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace IPInfoAPI_Codes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<Cache>();

            builder.Services.AddScoped<IPInfoServiceImpl>();
            builder.Services.AddScoped<IIPInfoService, Cache>();
            builder.Services.AddHostedService<IPInfoBackgroundService>();
            builder.Services.AddScoped<IIP2C,IP2CImpl>();
            builder.Services.AddMemoryCache();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            //LocalServer
            //builder.Services.AddDbContext<IPInfoAPIDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("IPInfoAPIConnectionString")));

            //Docker
            builder.Services.AddDbContext<IPInfoAPIDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DockerConnectionString")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}