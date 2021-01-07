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
    public class CommentController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository<Comment> repository;
        private readonly IMapper mapper;
        private readonly AppDbContext appContext;

        public CommentController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, IRepository<Comment> repository, IMapper mapper, AppDbContext appContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.repository = repository;
            this.mapper = mapper;
            this.appContext = appContext;
        }
        [Authorize]
        [HttpPost("Comment")]
        public IActionResult Create([FromBody]CommentCreateDTO comment)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(userId == null) { return Unauthorized(); }
            var tempComment = new Comment
            {
                PostId = comment.PostId,
                CommenterId = userId,
                Description = comment.Description
            };
            this.repository.Insert(tempComment);
            return Ok();
        }
        
    }
}
