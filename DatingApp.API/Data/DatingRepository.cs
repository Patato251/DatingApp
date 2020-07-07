using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class DatingRepository : IDatingRepository
  {
    private readonly DataContext _context;

    public DatingRepository(DataContext context)
    { 
      _context = context;
    }

    // Synchronous Add Function for users/photos
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }
    // Synchronous Delete Function for users/photos
    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<User> GetUser(int id)
    {
      // Finds the user and stores it in a local create var 
      // Include photos as part of return as it is navigation property, it won't be auto included
      // The id is passed to find the first or default user with the same id 
      // Default is equivalent to null, therefore returning either a user object or null object
      var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
      return user;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
      var users = await _context.Users.Include(p => p.Photos).ToListAsync();

      return users;
    }

    public async Task<bool> SaveAll()
    {
      // Returns true if saves were made, returns false if no saves where done
      return await _context.SaveChangesAsync() > 0;
    }
  }
}