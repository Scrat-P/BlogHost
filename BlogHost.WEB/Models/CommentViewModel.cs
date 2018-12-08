using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.WEB.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public UserViewModel Author { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public PostViewModel Post { get; set; }
    }
}
