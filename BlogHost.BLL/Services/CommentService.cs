using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Claims;
using BlogHost.BLL.DTO;
using BlogHost.DAL.Entities;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using BlogHost.BLL.Mappers;

namespace BlogHost.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        private UserDTO GetUser(ClaimsPrincipal currentUser)
        {
            return _userRepository.GetUser(currentUser).ToDTO();
        }

        public bool HasAccess(int? commentId, ClaimsPrincipal currentUser)
        {
            CommentDTO comment = _commentRepository.GetComment(commentId).ToDTO();

            var user = GetUser(currentUser);
            var userRoles = _userRepository.GetUserRoles(user.Id);

            return (user.Id == comment.Author.Id || userRoles.Contains("admin") || userRoles.Contains("moderator"));
        }

        public void Delete(int? id, ClaimsPrincipal currentUser)
        {
            if (HasAccess(id, currentUser))
            {
                _commentRepository.Delete(id);
            }
        }

        public CommentDTO GetComment(int? id, ClaimsPrincipal currentUser, bool checkAccess = true)
        {
            if (checkAccess && !HasAccess(id, currentUser))
            {
                return null;
            }
            return _commentRepository.GetComment(id).ToDTO();
        }

        public void Create(CommentDTO comment, ClaimsPrincipal currentUser, int postId)
        {
            CommentDTO databaseComment = new CommentDTO()
            {
                Author = GetUser(currentUser),
                Text = comment.Text,
                Created = DateTime.Now,
                Post = _postRepository.GetPost(postId).ToDTO()
            };
            _commentRepository.Create(databaseComment.ToEntity());
        }
    }
}
