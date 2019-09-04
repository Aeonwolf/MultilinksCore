/* 1. Add-Migration -Name InitialApiDbMigration -Context CoreDbContext -OutputDir Services/Migrations
 */

using Microsoft.EntityFrameworkCore;
using Multilinks.Core.Entities;

namespace Multilinks.Core.Services
{
   public class CoreDbContext : DbContext
   {
      public CoreDbContext(DbContextOptions<CoreDbContext> options)
          : base(options)
      {
      }

      public DbSet<EndpointClientEntity> Clients { get; set; }
      public DbSet<EndpointOwnerEntity> Owners { get; set; }
      public DbSet<EndpointEntity> Endpoints { get; set; }

      public DbSet<HubConnectionEntity> HubConnections { get; set; }
      public DbSet<EndpointLinkEntity> Links { get; set; }

      public DbSet<NotificationEntity> Notifications { get; set; }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         base.OnModelCreating(builder);
      }
   }
}
