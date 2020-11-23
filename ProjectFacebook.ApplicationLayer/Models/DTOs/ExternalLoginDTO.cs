using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Models.DTOs
{
    public class ExternalLoginDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ClaimsPrincipal Principal { get; set; }
    }
}
