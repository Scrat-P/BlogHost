using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogHost.BLL.DTO
{
    public class PostTagDTO
    {
        public int PostId { get; set; }
        public PostDTO Post { get; set; }

        public int TagId { get; set; }
        public TagDTO Tag { get; set; }
    }
}
