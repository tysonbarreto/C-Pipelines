using Backend.Models;
using Microsoft.EntityFrameworkCore;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// define services
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(optionsAction: option => option.UseSqlite(connectionString));

WebApplication app = builder.Build();


// define middlewares
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

app.Run();
