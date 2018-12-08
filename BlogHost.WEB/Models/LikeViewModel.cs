using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.WEB.Models
{
    public class LikeViewModel
    {
        public int Id { get; set; }
        public UserViewModel Author { get; set; }
        public PostViewModel Post { get; set; }
    }
}
