using Microsoft.AspNetCore.Mvc.Filters;
using Multilinks.Core.Filters;
using System;

namespace Multilinks.Core.Infrastructure
{
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
   public class EtagAttribute : Attribute, IFilterFactory
   {
      public bool IsReusable => true;

      public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
          => new EtagHeaderFilter();
   }
}
