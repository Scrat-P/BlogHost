using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlogHost.BLL.DTO;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.DAL.Entities;
using BlogHost.DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using BlogHost.BLL.Mappers;

namespace BlogHost.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IUserRepository userRepository, IPostRepository postRepository, IBlogRepository blogRepository)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        private ApplicationUser GetUser(ClaimsPrincipal currentUser)
        {
            return _userRepository.GetUser(currentUser);
        }

        private bool HasAccess(int? postId, ClaimsPrincipal currentUser)
        {
            PostDTO post = _postRepository.GetPost(postId).ToDTO();

            var user = GetUser(currentUser);
            var userRoles = _userRepository.GetUserRoles(user.Id);

            return (user.Id == post.Author.Id || userRoles.Contains("admin") || userRoles.Contains("moderator"));
        }

        public void Delete(int? id, ClaimsPrincipal currentUser)
        {
            if (HasAccess(id, currentUser))
            {
                _postRepository.Delete(id);
            }
        }

        public PostDTO GetPost(int? id, ClaimsPrincipal currentUser)
        {
            if (!HasAccess(id, currentUser))
            {
                return null;
            }
            return _postRepository.GetPost(id).ToDTO();
        }

        public void Edit(PostDTO post)
        {
            PostDTO databasePost = _postRepository.GetPost(post.Id).ToDTO();
            databasePost.Title = post.Title;
            databasePost.Text = post.Text;
            databasePost.LastUpdated = DateTime.Now;
            _postRepository.Update(databasePost.ToEntity());
        }

        public void Create(PostDTO post, ClaimsPrincipal currentUser, int blogId)
        {
            var currentTime = DateTime.Now;
            PostDTO databasePost = new PostDTO()
            {
                Title = post.Title,
                Text = post.Text,
                Blog = _blogRepository.GetBlog(blogId).ToDTO(),
                Author = GetUser(currentUser),
                Created = currentTime,
                LastUpdated = currentTime
            };
            _postRepository.Create(post.ToEntity());
        }

        public IEnumerable<PostDTO> GetPagePosts(int page, int pageSize, int blogId, out int postsCount)
        {
            IEnumerable<PostDTO> posts = _postRepository.GetPostList(blogId).ToDTO();
            postsCount = posts.Count();
            IEnumerable<PostDTO> postsPerPage = posts.Skip((page - 1) * pageSize).Take(pageSize);

            return postsPerPage;
        }
    }
}
