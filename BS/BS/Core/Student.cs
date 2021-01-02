using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Student : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
    public class Course : BaseEntity
    {
        public string CourseId { get; set; }
        public String CourseName { get; set; }
    }
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
