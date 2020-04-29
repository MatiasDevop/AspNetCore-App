using Example1.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email obligatory")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Format Incorret")]
        [EmailAddress]
        [Remote(action: "testEmail", controller: "Accounts")]
        [ValidateNameUser(user: "damit", ErrorMessage = "Word not permit")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [Compare("Password", ErrorMessage = "Password and Password of confirmation dosen't match up")]
        public string PasswordValid { get; set; }

        [Display(Name = "Help Password")]
        public string helpPass { get; set; }


    }
}
