using Microsoft.EntityFrameworkCore;
using Multilinks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Multilinks.Core.Services
{
   public class HubConnectionService : IHubConnectionService
   {
      private readonly CoreDbContext _context;

      public HubConnectionService(CoreDbContext context)
      {
         _context = context;
      }

      public async Task<bool> ConnectHubConnectionReferenceAsync(Guid endpointId, Guid ownerId, string connectionId, CancellationToken ct)
      {
         var endpoint = await _context.Endpoints
            .Where(r => r.EndpointId == endpointId && r.Owner.IdentityId == ownerId)
            .Include(r => r.HubConnection)
            .FirstOrDefaultAsync(ct);

         if (endpoint == null)
            return false;

         endpoint.HubConnection.ConnectionId = connectionId;
         endpoint.HubConnection.Connected = true;

         var updated = await _context.SaveChangesAsync(ct);
         if (updated < 1) return false;

         return true;
      }

      public async Task<bool> DisconnectHubConnectionReferenceAsync(string connectionId, CancellationToken ct)
      {
         var hubConnection = await _context.HubConnections.FirstOrDefaultAsync(r => r.ConnectionId == connectionId, ct);

         if (hubConnection != null)
         {
            hubConnection.ConnectionId = "";
            hubConnection.Connected = false;

            var updated = await _context.SaveChangesAsync(ct);
            if (updated < 1) return false;
         }

         return true;
      }

      public async Task<IEnumerable<EndpointLinkEntity>> GetActiveLinksConnectingToThisEndpointAsync(string connectionId,
         CancellationToken ct)
      {
         if (String.IsNullOrEmpty(connectionId)) return null;

         IQueryable<EndpointLinkEntity> query = _context.Links
            .Where(r => r.AssociatedEndpoint.HubConnection.ConnectionId == connectionId
               && r.Confirmed
               && r.SourceEndpoint.HubConnection.Connected)
            .Include(r => r.AssociatedEndpoint)
            .Include(r => r.SourceEndpoint).ThenInclude(r => r.HubConnection);

         var linksRequiredToBeNotified = await query.ToArrayAsync(ct);

         if (linksRequiredToBeNotified.Length == 0) return null;

         return linksRequiredToBeNotified;
      }
   }
}
