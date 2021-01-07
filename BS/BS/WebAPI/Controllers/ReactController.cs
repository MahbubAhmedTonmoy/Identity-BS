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
    public class ReactController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository<Like> repository;
        private readonly IRepository<Dislike> DislikeRepository;
        private readonly IMapper mapper;
        private readonly AppDbContext appContext;

        public ReactController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, IRepository<Like> repository, IRepository<Dislike> DislikeRepository, IMapper mapper, AppDbContext appContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.repository = repository;
            this.DislikeRepository = DislikeRepository;
            this.mapper = mapper;
            this.appContext = appContext;
        }
        [Authorize]
        [HttpPost("like")]
        public IActionResult GiveLike(LikeDTO like)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (userId == null) { return Unauthorized(); }
            var likeToCreate = new Like
            {
                CommentId = like.CommentId,
                LikerId = userId
            };
            repository.Insert(likeToCreate);
            return Ok();
        }
        [Authorize]
        [HttpPost("dislike")]
        public IActionResult GiveDislike(DislikeDTO dislike)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (userId == null) { return Unauthorized(); }
            var dislikeToCreate = new Dislike
            {
                CommentId = dislike.CommentId,
                DislikerId = userId
            };
            DislikeRepository.Insert(dislikeToCreate);
            return Ok();
        }

    }
}
