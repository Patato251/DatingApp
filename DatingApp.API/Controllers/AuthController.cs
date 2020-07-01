using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
  [Route("api/[controller]")] // http:localhost:5000/api/auth
  [ApiController] 
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;

    public AuthController(IAuthRepository repo) // Provide/inject context/data from AuthRepo
    {
        _repo = repo;
    }

    [HttpPost ("register")]
    public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
      // validate requests 

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
  }
}