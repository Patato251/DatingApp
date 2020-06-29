using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class DataContext : DbContext
  {
    // Constructor uses DbContext as the base options required for generating itself
    public DataContext(DbContextOptions<DataContext> options) : base (options){}

    // Telling class about entity (reference where our values are being pulled from)
    // Values == Table name created when scaffolding database
    public DbSet<Value> Values { get; set; } // Values is name for table generated
  }
}