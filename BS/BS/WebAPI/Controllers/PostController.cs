using AutoMapper;
using Core;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using test;
using WebAPI.Data;
using WebAPI.Infrastructure;

namespace WebAPI.Controllers
{
    public class PostController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository<Post> repository;
        private readonly IMapper mapper;
        private readonly AppDbContext appContext;

        public PostController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, IRepository<Post> repository, IMapper mapper, AppDbContext appContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.repository = repository;
            this.mapper = mapper;
            this.appContext = appContext;
        }
        [Authorize]
        [HttpPost("createPost")]
        public async Task<IActionResult> Create([FromBody]PostCreateDTO postCreateDTO)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(userId == null) { return Unauthorized(); }
            var post = new Post
            {
                UserId = userId,
                Description = postCreateDTO.Description
            };
            this.repository.Insert(post);
            return Ok();
        }
        [HttpPost("AllPost")]
        public async Task<IActionResult> GetAll()
        {
            // var post = repository.GetAll().Skip(1).Take(1);
            var post = appContext.Posts.Include(u => u.User.Email).Include(x => x.Comments).ThenInclude(x => x.Likes).Skip(1).Take(1);
            return Ok(post);
        }
    }
}
