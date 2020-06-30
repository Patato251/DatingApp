using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class AuthRespository : IAuthRespository
  {
    // Responsible for querying database via EF 
    private readonly DataContext _context;

    public AuthRespository(DataContext context)
    {
      _context = context;
    }

    public async Task<User> Login(string username, string password)
    {
      // Compare username to user in dB
      // Compare the hashed password with the password
      var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

      // If Username is null/no entry, return null entry
      if (username == null)
      {
        return null;
      }

      // Verify the password according to hash
      // If Password is null
      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
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
        // Translate the original password and determine if the hash is the same for the password being entered
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
        // Create method to create new password decryption
        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
      }

      private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
      {
        // Sample way to do using security cryptography hashing
        // Call using to have it be able to access the dispose function inside dependency and close once called
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
          passwordSalt = hmac.Key;
          passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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