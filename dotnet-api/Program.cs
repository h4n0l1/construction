using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMultipleOrigins",
        builder => builder.WithOrigins("http://localhost:8082", "https://dennytriana.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ConstructionContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowMultipleOrigins");

app.MapControllers();

app.Run();
