using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlogHost.BLL.DTO;

namespace BlogHost.BLL.ServiceInterfaces
{
    public interface ICommentService
    {
        bool HasAccess(int? commentId, ClaimsPrincipal currentUser);
        void Delete(int? id, ClaimsPrincipal currentUser);
        CommentDTO GetComment(int? id, ClaimsPrincipal currentUser, bool checkAccess = true);
        void Create(CommentDTO comment, ClaimsPrincipal currentUser, int postId);
    }
}
