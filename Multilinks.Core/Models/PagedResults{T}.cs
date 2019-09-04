using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multilinks.Core.Models
{
   public class PagedResults<T>
   {
      public IEnumerable<T> Items { get; set; }

      public int TotalSize { get; set; }
   }
}
