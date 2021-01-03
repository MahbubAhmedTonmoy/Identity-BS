using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    public class PostCreateDTO
    {
        [Required]
        public string Description { get; set; }
    }
}
