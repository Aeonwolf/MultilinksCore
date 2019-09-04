using Multilinks.Core.Infrastructure;
using Newtonsoft.Json;

namespace Multilinks.Core.Models
{
   public class RootResponse : Resource, IEtaggable
   {
      public Link Endpoints { get; set; }

      public string GetEtag()
      {
         var serialized = JsonConvert.SerializeObject(this);
         return Md5Hash.ForString(serialized);
      }
   }
}
