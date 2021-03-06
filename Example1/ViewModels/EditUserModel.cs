﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class EditUserModel
    {
        public EditUserModel()
        {
            Notifications = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string helpPass { get; set; }
        public List<string> Notifications { get; set; }
        public IList<string> Roles { get; set; }

    }
}
