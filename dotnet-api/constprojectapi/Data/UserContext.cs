using constprojectapi.Models;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Data
{
  public class UserContext : DbContext
  {
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}