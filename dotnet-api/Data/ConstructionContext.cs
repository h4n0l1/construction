using Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  public class ConstructionContext : DbContext
  {
    public ConstructionContext(DbContextOptions<ConstructionContext> options) : base(options)
    {
    }

    public DbSet<Construction> Constructions { get; set; }
  }
}