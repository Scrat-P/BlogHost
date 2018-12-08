using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;
using BlogHost.BLL.DTO;

namespace BlogHost.BLL.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public UserDTO Author { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public BlogDTO Blog { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<LikeDTO> Likes { get; set; }

        public List<PostTagDTO> Tags { get; set; }
    }
}
