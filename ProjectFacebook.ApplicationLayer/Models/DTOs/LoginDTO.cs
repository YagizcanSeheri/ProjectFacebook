using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Models.DTOs
{
    public class LoginDTO
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
