using AutoMapper;
using Core;
using Core.DTO;
using test;
using WebAPI.DTO;

namespace WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CourseCreateDTO, Course>();
            CreateMap<Post, PostView>();
        }
    }
}
