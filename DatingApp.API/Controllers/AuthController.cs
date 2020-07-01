using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
  [Route("api/[controller]")] // http:localhost:5000/api/auth
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;

    public AuthController(IAuthRepository repo, IConfiguration config) // Provide/inject context/data from AuthRepo
    {
      _config = config;
      _repo = repo;
    }

    /*REGISTER METHOD*/
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
      // validate requests to prevent requests that should provide errors from occuring 
      // E.g. Blank username or password

      // Only if APIController is not used
      /* if (!ModelState.IsValid)
            return BadRequest(ModelState)
      */

      // Make the username lowercase to prevent duplicate entries
      userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

      // Check in the database if username exists 
      if (await _repo.UserExists(userForRegisterDto.Username))
      {
        return BadRequest("Username already Exists");
      }

      // Create our user 
      var userToCreate = new User
      {
        Username = userForRegisterDto.Username // Only data we can store in new User object
      };

      // Password creation is generated from repo password generation 
      var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password); // Register method from IAuthRepository

      // Need to return routename and the entity itself
      // return CreatedAtRoute();

      return StatusCode(201);
    }

    /*LOGIN METHOD*/
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
      var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

      // If no user detected/doesn't exist
      if (userFromRepo == null)
      {
        return Unauthorized();
      }

      // Make claim in order to begin generation of the key to be sent back to the user
      // Claim Username ID and username itself from the userFromRepo that is currently stored in the method
      var claims = new[]
      {
        new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
        new Claim(ClaimTypes.Name, userFromRepo.Username)
      };

      // Integrate a hashed key to the token in order to encrypt it 
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

      // Generate Signing Credentials in order to allow the API to know it signed the original key generation and therefore it is valid if received again
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      // Create Security Token Descriptor
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims), // The detaisl/claims associated with the token
        Expires = DateTime.Now.AddDays(1), // 24 hr expiry date
        SigningCredentials = creds // Credentials passed through with the signature
      };

      // Token Handler to allow for generation of the token
      var tokenHandler = new JwtSecurityTokenHandler();

      // Pass in the token descriptor through the token handler
      var token = tokenHandler.CreateToken(tokenDescriptor); // This has the Jwt Token in it's entirety

      // Return the token in it's entirety by writing the token and returning the token as an object
      return Ok(new {
        token = tokenHandler.WriteToken(token)
      });
    }
  }
}