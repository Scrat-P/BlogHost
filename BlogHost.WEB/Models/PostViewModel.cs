using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;
using Microsoft.AspNetCore.Http;
using BlogHost.WEB.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BlogHost.WEB.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public UserViewModel Author { get; set; }

        [Required]
        [Title(ErrorMessage = "The {0} field contains illegal characters.")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(50000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Text { get; set; }

        public int BlogId { get; set; }
        public BlogViewModel Blog { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public byte[] ProfilePicture { get; set; }
        public IFormFile LoadableProfilePicture { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        public List<LikeViewModel> Likes { get; set; }

        public List<PostTagViewModel> Tags { get; set; }
    }
}
