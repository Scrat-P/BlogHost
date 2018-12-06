using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.BLL.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PostDTO> Posts { get; set; }
    }
}
