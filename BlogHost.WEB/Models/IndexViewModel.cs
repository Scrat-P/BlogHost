using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogHost.WEB.Models
{
    public class IndexViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string Author { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
        public string Title { get; set; }
    }
}
