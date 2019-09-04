using Microsoft.AspNetCore.Http;

namespace Multilinks.Core.Infrastructure
{
   public static class HttpRequestExtensions
   {
      public static IEtagHandlerFeature GetEtagHandler(this HttpRequest request)
          => request.HttpContext.Features.Get<IEtagHandlerFeature>();
   }
}
