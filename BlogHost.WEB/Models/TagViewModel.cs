using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogHost.WEB.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PostTagViewModel> Posts { get; set; }
    }
}
