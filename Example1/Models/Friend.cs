using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class Friend
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obligatorio"), MaxLength(100, ErrorMessage = "No more than 100 charts")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Obligatorio")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Format Incorret")]
        public string Email { get; set; }

        [Required(ErrorMessage = "It must be a city")]
        public Province? City { get; set; }

        public string routePhoto { get; set; }
    }
}
