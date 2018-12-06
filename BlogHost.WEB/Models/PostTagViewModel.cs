using System;
using System.Collections.Generic;
using System.Linq;
using BlogHost.DAL.Entities;
using System.Threading.Tasks;

namespace BlogHost.WEB.Models
{
    public class PostTagViewModel
    {
        public int PostId { get; set; }
        public PostViewModel Post { get; set; }

        public int TagId { get; set; }
        public TagViewModel Tag { get; set; }
    }
}
