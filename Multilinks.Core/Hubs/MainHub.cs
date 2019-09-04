using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Multilinks.Core.Hubs.Interfaces;
using Multilinks.Core.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Multilinks.Core.Hubs
{
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
   public class MainHub : Hub<IMainHub>
   {
      private readonly IHubConnectionService _hubConnectionService;

      public MainHub(IHubConnectionService hubConnectionService)
      {
         _hubConnectionService = hubConnectionService;
      }

      public override async Task OnConnectedAsync()
      {
         var endpointId = Context.GetHttpContext().Request.Query["ep"];
         var ownerId = Context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

         if (string.IsNullOrEmpty(endpointId) || string.IsNullOrEmpty(ownerId))
         {
            Context.Abort();
         }

         var connectionReferenceCreated = await _hubConnectionService.ConnectHubConnectionReferenceAsync(
            Guid.Parse(endpointId),
            Guid.Parse(ownerId),
            Context.ConnectionId,
            Context.ConnectionAborted);

         if (!connectionReferenceCreated)
         {
            Context.Abort();
         }

         await NotifySourceEnpointsOfLinkConnectedState();

         await base.OnConnectedAsync();
      }

      public override async Task OnDisconnectedAsync(Exception exception)
      {
         await NotifySourceEnpointsOfLinkDisconnectedState();

         /* TODO: Should log if false. */
         var connectionReferenceDeleted = await _hubConnectionService.DisconnectHubConnectionReferenceAsync(
            Context.ConnectionId,
            CancellationToken.None);

         await base.OnDisconnectedAsync(exception);
      }

      private async Task NotifySourceEnpointsOfLinkConnectedState()
      {
         await NotifySourceEnpointsOfLinkConnectionState(true);
      }

      private async Task NotifySourceEnpointsOfLinkDisconnectedState()
      {
         await NotifySourceEnpointsOfLinkConnectionState(false);
      }

      private async Task NotifySourceEnpointsOfLinkConnectionState(bool state)
      {
         var links = await _hubConnectionService.GetActiveLinksConnectingToThisEndpointAsync(Context.ConnectionId,
            CancellationToken.None);

         if (links != null)
         {
            foreach (var link in links)
            {
               await Clients.Client(link.SourceEndpoint.HubConnection.ConnectionId)
                  .LinkActiveStateReceived(link.LinkId.ToString(), state);
            }
         }
      }
   }
}