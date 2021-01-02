using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Infrastructure;

namespace WebAPI.Controllers
{
    public class CourseController : ControllerBase
    {
        private readonly IRepository<Course> repository;
        private readonly IMapper mapper;

        public CourseController(IRepository<Course> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpPost("GetCourses")]
        public ActionResult<IEnumerable<Course>> GetAll()
        {
            var result = this.repository.GetAll();
            return Ok(result);
        }
        [HttpPost("createCourse")]
        public ActionResult CourseCreate(CourseCreateDTO course)
        {
            this.repository.Insert(mapper.Map<Course>(course));
            return Ok(course);
        }
    }
}
