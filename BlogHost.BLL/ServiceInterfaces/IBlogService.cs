using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlogHost.BLL.DTO;

namespace BlogHost.BLL.ServiceInterfaces
{
    public interface IBlogService
    {
        void Delete(int? id, ClaimsPrincipal currentUser);
        BlogDTO GetBlog(int? id, ClaimsPrincipal currentUser);
        void Edit(BlogDTO blog);
        void Create(BlogDTO blog, ClaimsPrincipal currentUser);
        IEnumerable<BlogDTO> GetPageBlogs(int page, int pageSize, ClaimsPrincipal currentUser, out int blogsCount);
    }
}
