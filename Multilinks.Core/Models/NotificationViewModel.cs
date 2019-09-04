using Multilinks.Core.Infrastructure;
using Newtonsoft.Json;
using System;

namespace Multilinks.Core.Models
{
   public class NotificationViewModel : Resource, IEtaggable
   {
      public enum Type
      {
         None,
         LinkRequestAccepted,
         LinkRequestDenied
      }

      public Guid Id { get; set; }

      public Type NotificationType { get; set; }

      public string Message { get; set; }

      public bool Hidden { get; set; }

      public string GetEtag()
      {
         var serialized = JsonConvert.SerializeObject(this);
         return Md5Hash.ForString(serialized);
      }
   }
}
