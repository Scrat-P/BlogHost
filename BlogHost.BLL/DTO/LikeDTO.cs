using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.BLL.DTO
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }
        public PostDTO Post { get; set; }
    }
}
