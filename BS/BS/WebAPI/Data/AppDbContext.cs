using Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace WebAPI.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Add-Migration InitialCreate
            //Update-Database
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
    
}
