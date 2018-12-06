using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlogHost.BLL.DTO;

namespace BlogHost.BLL.ServiceInterfaces
{
    public interface IPostService
    {
        void Delete(int? id, ClaimsPrincipal currentUser);
        PostDTO GetPost(int? id, ClaimsPrincipal currentUser);
        void Edit(PostDTO post);
        void Create(PostDTO post, ClaimsPrincipal currentUser, int blogId);
        IEnumerable<PostDTO> GetPagePosts(int page, int pageSize, int blogId, out int postsCount);
    }
}
