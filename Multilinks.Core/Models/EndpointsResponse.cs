using Multilinks.Core.Infrastructure;
using Newtonsoft.Json;

namespace Multilinks.Core.Models
{
   public class EndpointsResponse : PagedCollection<EndpointViewModel>, IEtaggable
   {
      public string GetEtag()
      {
         var serialized = JsonConvert.SerializeObject(this);
         return Md5Hash.ForString(serialized);
      }
   }
}
