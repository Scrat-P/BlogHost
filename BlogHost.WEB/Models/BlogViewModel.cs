using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.WEB.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
