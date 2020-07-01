using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class AuthRepository : IAuthRepository
  {
    // Responsible for querying database via EF 
    private readonly DataContext _context;

    // Inject Datacontext to be used (the table to be used)  
    public AuthRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<User> Login(string username, string password)
    {
      // Compare username to user in dB
      // Compare the hashed password with the password
      var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username); // Accessing the user data by finding it in DB

      // If User is null/no entry, return null entry
      if (user == null)
      {
        return null;
      }

      // Verify the password according to hash
      // If Password is null
      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) // Using the password stashed to the user
      {
        return null;
      }

      return user;
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      // Terminate once completed function
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        // Translate the original password into hash and compare it to the stored hash in the DB
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        // Comparison of the password hash's length as they are byte/array of certain length
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i]) return false;
        }
      }

      return true;
    }

    public async Task<User> Register(User user, string password)
    {
      byte[] passwordHash, passwordSalt;
      // Create method to create new password decryption (use out not ref as two variables have not been initialised)
      CreatePasswordHash(password, out passwordHash, out passwordSalt); // Passing reference of Hash and Salt, as we have not initialised yet, and we want to return the two

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      // Sample way to do using security cryptography hashing
      // Call using method to dispose/destroy the object block by hitting an exception or when function completed
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // getting string password as byte array with UTF8 encoding to make hash
      }
    }

    public async Task<bool> UserExists(string username)
    {
      if (await _context.Users.AnyAsync(x => x.Username == username))
        return true;

      return false;
    }
  }
}