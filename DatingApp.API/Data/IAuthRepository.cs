using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
  public interface IAuthRepository
  {
    /* Writing the methods to be used by controller */
    Task<User> Register(User user, string password);  // Add Method
    Task<User> Login(string username, string password); // Get Method
    Task<bool> UserExists(string username); // Find Method
  }
}