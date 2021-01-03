using Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using test;
using test2;

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
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }

        public DbSet<SemesterReg> SemesterRegs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasMany(x => x.Posts).WithOne(x => x.User);
            modelBuilder.Entity<Post>().HasMany(x => x.Comments).WithOne(x => x.Post);

            modelBuilder.Entity<Comment>().HasMany(x => x.Likes).WithOne(x => x.Comment);
            modelBuilder.Entity<Comment>().HasMany(x => x.Dislikes).WithOne(x => x.Comment);
        }
    }
    
}
