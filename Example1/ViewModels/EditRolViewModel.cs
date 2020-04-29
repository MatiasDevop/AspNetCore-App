using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class EditRolViewModel
    {
        public string Id { get; set; }
        public EditRolViewModel()
        {
            Users = new List<string>();
        }

        [Required(ErrorMessage = "The Name of Rol is Must")]
        public string NameRol { get; set; }
        public List<string> Users { get; set; }
    }
}
