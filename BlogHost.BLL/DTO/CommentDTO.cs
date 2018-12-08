using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public UserDTO Author { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public PostDTO Post { get; set; }
    }
}
