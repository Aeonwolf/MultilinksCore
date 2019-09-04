using System.ComponentModel.DataAnnotations;

namespace Multilinks.Core.Models
{
   public class NewEndpointLinkForm
   {
      [Required]
      [Display(Name = "source", Description = "Endpoint id of request originator")]
      public string Source { get; set; }

      [Required]
      [Display(Name = "destination", Description = "Endpoint id of request destination")]
      public string Destination { get; set; }
   }
}
