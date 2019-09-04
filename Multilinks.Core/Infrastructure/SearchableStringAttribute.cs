using System;

namespace Multilinks.Core.Infrastructure
{
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class SearchableStringAttribute : SearchableAttribute
   {
      public SearchableStringAttribute()
      {
         ExpressionProvider = new StringSearchExpressionProvider();
      }
   }
}
