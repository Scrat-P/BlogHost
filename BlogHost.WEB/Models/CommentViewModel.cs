using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogHost.WEB.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public UserViewModel Author { get; set; }

        [Required]
        [RegularExpression(@"[\w\s]*")]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Text { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public PostViewModel Post { get; set; }
    }
}
