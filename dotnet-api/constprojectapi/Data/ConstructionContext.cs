using constprojectapi.Models;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Data
{
  public class ConstructionContext : DbContext
  {
    public ConstructionContext(DbContextOptions<ConstructionContext> options) : base(options)
    {
    }

    public DbSet<Construction> Constructions { get; set; }
  }
}