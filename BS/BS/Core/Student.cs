using Core;
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

namespace test
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime PostDate { get; set; }
        public string Description { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string CommenterId { get; set; }
        public AppUser Commenter { get; set; }
        public Post Post { get; set; }
        public DateTime CommentDate { get; set; }
        public string Description { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Dislike> Dislikes { get; set; }
    }
    public class Like
    {
        public int Id { get; set; }
        public string LikerId { get; set; }
        public AppUser Liker { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
    public class Dislike
    {
        public int Id { get; set; }
        public string DislikerId { get; set; }
        public AppUser Disliker { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
namespace test2
{
    /// <summary>
    /// ai system e student and teacher table e semesterReg tale er primary key porbe
    /// </summary>
    //public class SemesterReg : BaseEntity
    //{
    //    public int SemesterNumber { get; set; }
    //    public List<Students> Students { get; set; }
    //    public List<Teachers> Teachers { get; set; }
    //}
    ///<summary>
    /// table colum : Id, createdate, smesternumber, studentId, teacherId
    ///</summary>
    public class SemesterReg : BaseEntity
    {
        public int SemesterNumber { get; set; }
    }
    public class Students : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Semester { get; set; }
        public double CGPA { get; set; }
        public List<SemesterReg> semesterRegs { get; set; }
    }
    public class Teachers: BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Semester { get; set; }
        public List<SemesterReg> semesterRegs { get; set; }
    }
    public class Courses : BaseEntity
    {
        public string CourseId { get; set; }
        public String CourseName { get; set; }
        public List<Students> Student { get; set; }
        public List<Teachers> Teachers { get; set; }
    }
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
