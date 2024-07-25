using constprojectapi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var allowedHost1 = System.Environment.GetEnvironmentVariable("ALLOWED_HOST1");
var allowedHost2 = System.Environment.GetEnvironmentVariable("ALLOWED_HOST2");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMultipleOrigins",
        builder => builder.WithOrigins(allowedHost1, allowedHost2)
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
