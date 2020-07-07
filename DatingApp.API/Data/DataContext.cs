using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

// Use this file to let EntityFramework know of development of new class in Models
namespace DatingApp.API.Data
{
  public class DataContext : DbContext
  {
    /* 
     * Constructor uses DbContext as the base options required for generating itself
     * Derives from use of DbContext from Entityframwork core to take required properties
     * Creates context needed to access required database later
     */
    public DataContext(DbContextOptions<DataContext> options) : base (options){}

    // Telling class about entity (reference where our values are being pulled from)
    // Values == Table name created when scaffolding database
    public DbSet<Value> Values { get; set; } // Values is name for table generated

    public DbSet<User> Users { get; set; } // Table generated to store User data within database

    public DbSet<Photo> Photos { get; set; }

    /* After every addition of a DbSet, need to migrate database to add new table of data*/
  }
}