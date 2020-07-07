using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
  public interface IDatingRepository
  {
    // Creating Generic methods using T as input and restricted to class entities
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T: class;
    // Saving 0 or more than 0 changes, and if it is true, it will save the required changes
    Task<bool> SaveAll();
    // Get all Users
    Task<IEnumerable<User>> GetUsers();
    // Get a single user
    Task<User> GetUser(int id);
  }
}