using System.ComponentModel.DataAnnotations;

namespace Multilinks.Core.Models
{
   public class NewEndpointForm
   {
      [Required]
      [Display(Name = "name", Description = "Name of endpoint")]
      public string Name { get; set; }

      [Required]
      [Display(Name = "description", Description = "Short description of endpoint")]
      public string Description { get; set; }
   }
}
