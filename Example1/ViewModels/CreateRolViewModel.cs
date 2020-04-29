using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class CreateRolViewModel
    {
        [Required(ErrorMessage = "This field is Obligatory")]
        [Display(Name = "Rol")]
        public string NameRole { get; set; }

    }
}
