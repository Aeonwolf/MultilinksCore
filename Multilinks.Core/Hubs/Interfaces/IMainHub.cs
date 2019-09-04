using Multilinks.Core.Entities;
using System.Threading.Tasks;

namespace Multilinks.Core.Hubs.Interfaces
{
   public interface IMainHub
   {
      Task LinkRequestReceived(string linkId, string sourceDeviceName, string sourceDeviceOwnerName);

      Task LinkConfirmationReceived(string linkId, string associatedDeviceName, string associatedDeviceOwnerName, bool isActive);

      Task NotificationReceived(string id, NotificationEntity.Type notificationType, string message, bool hidden);

      Task LinkActiveStateReceived(string linkId, bool isActive);
   }
}
