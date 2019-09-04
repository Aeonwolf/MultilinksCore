using System;

namespace Multilinks.Core.Infrastructure
{
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class SearchableBooleanAttribute : SearchableAttribute
   {
      public SearchableBooleanAttribute()
      {
         ExpressionProvider = new BooleanSearchExpressionProvider();
      }
   }
}
