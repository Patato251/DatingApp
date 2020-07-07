using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
  public static class Extensions
  {
    public static void AddApplicationError(this HttpResponse response, string message) 
    {
      // Adding additional headers to the response produced via the default http response that we recieve within the website
      response.Headers.Add("Application-Error", message);
      // Adding CORS headers for allowing authentication/validation data
      response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
      response.Headers.Add("Acess-Control-Allow-Origin", "*");
    }

    // Age Calculator Method extension to DateOfBirth
    public static int CalculateAge(this DateTime theDateTime)
    {
      // Calculates Age
      var age = DateTime.Today.Year - theDateTime.Year;
      // Check if birthday occurred
      if (theDateTime.AddYears(age) > DateTime.Today)
      {
        age--;
      }
      
      return age;
    }
  }
}