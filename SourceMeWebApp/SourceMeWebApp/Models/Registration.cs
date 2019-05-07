using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceMeWebApp.Models
{
    public class Registration
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string LastName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid")]
        public string  UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        

    }
}
