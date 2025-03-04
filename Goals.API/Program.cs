
using Goals.API.Abstractions.Helpers;
using Goals.API.Abstractions.Repositories;
using Goals.API.Abstractions.Services;
using Goals.API.Context;
using Goals.API.Helpers;
using Goals.API.Middlewares;
using Goals.API.Repositories;
using Goals.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Goals.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false);
            // Add services to the container.
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MsSql") ?? throw new Exception("ConnectionsStrings:MsSql Is Null");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtHelper, JwtHelper>();

            var app = builder.Build();

            app.UseMiddleware<HttpExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
