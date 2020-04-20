using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email obligatory")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberPassword { get; set; }
    }
}
