using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.DAL.RepositoryInterfaces
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetBlogList(ApplicationUser user);
        void Create(Blog blog, ApplicationUser user);
        int? GetBlogId(string title);
        Blog GetBlog(int? id); 
        void Update(Blog blog);
        void Delete(int? id);
        void Save();
    }
}
