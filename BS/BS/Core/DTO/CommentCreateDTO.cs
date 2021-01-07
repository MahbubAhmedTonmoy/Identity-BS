using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class CommentCreateDTO
    {
        public int PostId { get; set; }
        public string Description { get; set; }
    }
    public class PostView
    {
        public string Description { get; set; }
        //public List<CommentDetails> Comments { get; set; }
    }
    public class CommentDetails
    {
        public string Description { get; set; }
    }
    public class LikeDTO
    {
        [Required]
        public int CommentId { get; set; }
    }
    public class DislikeDTO
    {
        [Required]
        public int CommentId { get; set; }
    }
}
