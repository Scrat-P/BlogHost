using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.WEB.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public UserViewModel Author { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int BlogId { get; set; }
        public BlogViewModel Blog { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        public List<LikeViewModel> Likes { get; set; }

        public List<PostTagViewModel> Tags { get; set; }
    }
}
