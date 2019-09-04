
using Multilinks.Core.Entities;
using Multilinks.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multilinks.Core.Services
{
   public interface INotificationService
   {
      Task<bool> CreateNotificationAsync(NotificationEntity notification, CancellationToken ct);

      Task<NotificationEntity> GetNotificationByIdAsync(Guid id, CancellationToken ct);

      Task<PagedResults<NotificationEntity>> GetNewNotificationsAsync(Guid endpointId,
         Guid ownerId,
         PagingOptions pagingOptions,
         CancellationToken ct);

      Task<bool> UpdateHiddenStatusByIdAsync(Guid id, bool hidden, CancellationToken ct);
   }
}
