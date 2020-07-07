using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
  public class Seed
  {
    public static void SeedUsers(DataContext context)
    {
      // Ensure no existing data exists to not generate repeated info
      {
        if (!context.Users.Any())
        {
          var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
          var users = JsonConvert.DeserializeObject<List<User>>(userData);
          foreach (var user in users)
          {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("password", out passwordHash, out passwordSalt);
          
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = user.Username.ToLower();
            context.Users.Add(user);
          }
          // Can be left as synchronous as process is required to run before any access provided
          context.SaveChanges();
        }
      }
    }

    // Taken from AuthRepository to mimic data collection and generation
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      // Sample way to do using security cryptography hashing
      // Call using method to dispose/destroy the object block by hitting an exception or when function completed
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // getting string password as byte array with UTF8 encoding to make hash
      }
    }
  }
}