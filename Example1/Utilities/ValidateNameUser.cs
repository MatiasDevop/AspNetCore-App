using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Utilities
{
    public class ValidateNameUser: ValidationAttribute // if you wanna customizar validates
    {
        private readonly string user;
        public ValidateNameUser(string user)
        {
            this.user = user;
        }

        public override bool IsValid(object value)
        {
            Boolean allowed = true;
            if (value.ToString().Contains("damit"))
                allowed = false;

            return allowed;
        }
    }
}
