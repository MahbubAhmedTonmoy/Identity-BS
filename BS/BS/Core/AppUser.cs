using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using test;

namespace Core
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string RefreshToken { get; set; }
        public ICollection<Post> Posts{get; set;}
    }
}
