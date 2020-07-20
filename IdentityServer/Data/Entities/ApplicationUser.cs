using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(300)]
        public string DisplayName { get; set; }
    }
}
