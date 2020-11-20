using Microsoft.AspNetCore.Identity;
using System;

namespace Core
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
