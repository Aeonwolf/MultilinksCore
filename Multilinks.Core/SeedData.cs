using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Multilinks.Core.Services;
using System;

namespace Multilinks.Core
{
   public class SeedData
   {
      public static void EnsureSeedData(IServiceProvider serviceProvider)
      {
         Console.WriteLine("Seeding database...");

         using(var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
         {
            var context = scope.ServiceProvider.GetService<CoreDbContext>();
            context.Database.Migrate();
         }

         Console.WriteLine("Done seeding database.");
         Console.WriteLine();
      }
   }
}
