﻿using System.ComponentModel.DataAnnotations;

namespace Multilinks.TokenService.Models.AccountViewModels
{
   public class RegisterConfirmationViewModel
   {
      [Required]
      [Display(Name = "First Name")]
      public string FirstName { get; set; }

      [Required]
      [Display(Name = "Last Name")]
      public string LastName { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }

      public string UserId { get; set; }
      public string Code { get; set; }
   }
}