using System.Configuration;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication3.Controllers;
using WebApplication3.Entities;
using WebApplication3.Repositories;

public class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services
            .AddScoped<IArticleRepository, ArticleRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ITagRepository, TagRepository>();


// Add services to the container.

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain
            .GetAssemblies());

        builder.Services.AddCors(
            options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }
                );
            }
        );

        builder.Services.AddControllers();
        builder.Services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddDbContext<DBContext>(x =>
                x.UseMySql("server=localhost;port=3306;database=dotnet;user=root;password=Weiyi123",
                    ServerVersion.AutoDetect(
                        "server=localhost;port=3306;database=dotnet;user=root;password=Weiyi123")));

        var app = builder.Build();
        app.UseCors();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DBContext>();
           
            await db.Database.EnsureCreatedAsync();
        }


// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors(MyAllowSpecificOrigins);
        app.MapControllers();

        app.Run();
    }
}