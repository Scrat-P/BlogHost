using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;
using BlogHost.WEB.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BlogHost.WEB.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public UserViewModel Author { get; set; }

        [Required]
        [Title(ErrorMessage = "The {0} field contains illegal characters.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Description { get; set; }

        public List<PostViewModel> Posts { get; set; }
    }
}
