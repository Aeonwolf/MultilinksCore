using Multilinks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multilinks.Core.Services
{
   public interface IHubConnectionService
   {
      Task<bool> ConnectHubConnectionReferenceAsync(Guid endpointId, Guid ownerId, string connectionId, CancellationToken ct);

      Task<bool> DisconnectHubConnectionReferenceAsync(string connectionId, CancellationToken ct);

      Task<IEnumerable<EndpointLinkEntity>> GetActiveLinksConnectingToThisEndpointAsync(string connectionId,
         CancellationToken ct);
   }
}
