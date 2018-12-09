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
        bool HasAccess(int? postId, ClaimsPrincipal currentUser);
        void Delete(int? id, ClaimsPrincipal currentUser);
        PostDTO GetPost(int? id, ClaimsPrincipal currentUser, bool checkAccess = true);
        void Edit(PostDTO post, ICollection<string> tags);
        void Create(PostDTO post, ClaimsPrincipal currentUser, int blogId, ICollection<string> tags);
        IEnumerable<PostDTO> GetPagePosts(int page, int pageSize, int blogId, out int postsCount);
        void Like(int id, ClaimsPrincipal currentUser);
        void Unlike(int id, ClaimsPrincipal currentUser);
        bool IsLiked(int id, ClaimsPrincipal currentUser);
    }
}
