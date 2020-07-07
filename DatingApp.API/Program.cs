using System;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApp.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // Assigned the host building to a var to be used later 
      var host = CreateHostBuilder(args).Build();

      // Want to inject instance of datacontext since we cant use in main method 
      // Want to also dispose of datacontext as soon as done with it
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        // Add try catch block to catch errors, must prevent any issues from occuring as well as catch error handling
        try
        {
          // Assign context
          var context = services.GetRequiredService<DataContext>();
          // Migrate the database pending and create the database if it doesn't exist automatically
          context.Database.Migrate();
          // Seeding the users with the data we require
          Seed.SeedUsers(context);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error has occurred during the migration process");
        }
        host.Run();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
