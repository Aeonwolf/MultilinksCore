using System.ComponentModel.DataAnnotations;

namespace Multilinks.Core.Models
{
   public class UpdateNotificationForm
   {
      [Required]
      [Display(Name = "isHidden", Description = "Don't show this notification to the intended recipient.")]
      public bool Hidden { get; set; }
   }
}
