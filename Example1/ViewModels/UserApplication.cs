using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.ViewModels
{
    public class UserApplication:IdentityUser
    {
        public string helpPass { get; set; }
    }
}
