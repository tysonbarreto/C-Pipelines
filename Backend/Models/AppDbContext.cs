using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Person> People {get; set;}
    
}