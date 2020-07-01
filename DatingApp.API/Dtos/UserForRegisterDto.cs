using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
  public class UserForRegisterDto
  {
    // Validation step to have it so this entry field requires a string
    [Required]
    public string Username { get; set; }

    // Can also specify error and requirements for the required field 
    [Required]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 to 8 characters in length")]
    public string Password { get; set; }
  }
}