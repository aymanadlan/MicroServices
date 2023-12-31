
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app,bool IsProduction)
        {
          using(var serviceScope= app.ApplicationServices.CreateScope())
          {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),IsProduction);
            
          }
        }

        public static void SeedData(AppDbContext context,bool IsProduction)
        {
            //**
            if (IsProduction)
            {
              System.Console.WriteLine("--> Attempting to apply migrations");
              try
              {
                context.Database.Migrate();
              }
              catch (System.Exception ex)
              {
                System.Console.WriteLine($"--> could not run migration {ex.Message}");
              }
            }
            //**

            if (!context.Platforms.Any())
          {
            System.Console.WriteLine("--> Seeding Data");

            context.Platforms.AddRange(
                new Platform()
                {
                    Name = "Dot Net",
                    Publisher="Microsoft",
                    Cost="Free"
                },
                new Platform()
                {
                    Name = "Sql Server Express",
                    Publisher="Microsoft",
                    Cost="Free"
                },
                new Platform()
                {
                    Name = "Kubernetes",
                    Publisher="Cloud Native Computing Foundation",
                    Cost="Free"
                }
            );
            context.SaveChanges();
          }
          else
          {
            System.Console.WriteLine("--> We already have data");
          }
        }
    }
}