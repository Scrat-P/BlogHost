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

        private UserDTO GetUser(ClaimsPrincipal currentUser)
        {
            return _userRepository.GetUser(currentUser).ToDTO();
        }

        public bool HasAccess(int? postId, ClaimsPrincipal currentUser)
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

        public PostDTO GetPost(int? id, ClaimsPrincipal currentUser, bool checkAccess = true)
        {
            if (checkAccess && !HasAccess(id, currentUser))
            {
                return null;
            }
            return _postRepository.GetPost(id).ToDTO();
        }

        public void Edit(PostDTO post, ICollection<string> tags)
        {
            _postRepository.Update(post.ToEntity(), tags);
        }

        public void Create(PostDTO post, ClaimsPrincipal currentUser, int blogId, ICollection<string> tags)
        {
            var currentTime = DateTime.Now;
            PostDTO databasePost = new PostDTO()
            {
                Title = post.Title,
                Text = post.Text,
                Blog = _blogRepository.GetBlog(blogId).ToDTO(),
                Author = GetUser(currentUser),
                Created = currentTime,
                LastUpdated = currentTime,
                ProfilePicture = post.ProfilePicture
            };
            _postRepository.Create(databasePost.ToEntity(), tags);
        }

        public IEnumerable<PostDTO> GetPopularWeekPosts(int page, int pageSize, out int postsCount)
        {
            IEnumerable<PostDTO> posts = _postRepository
                .GetPopularWeekPostsList().ToDTO()
                .OrderBy(post => post.Likes.Count + post.Comments.Count)
                .Reverse();
            postsCount = posts.Count();
            IEnumerable<PostDTO> postsPerPage = posts.Skip((page - 1) * pageSize).Take(pageSize);

            return postsPerPage;
        }

        public IEnumerable<PostDTO> GetPagePosts(int page, int pageSize, int blogId, out int postsCount)
        {
            IEnumerable<PostDTO> posts = _postRepository.GetPostList(blogId).ToDTO();
            postsCount = posts.Count();
            IEnumerable<PostDTO> postsPerPage = posts.Skip((page - 1) * pageSize).Take(pageSize);

            return postsPerPage;
        }

        public void Like(int id, ClaimsPrincipal currentUser)
        {
            _postRepository.Like(id, _userRepository.GetUser(currentUser));
        }

        public void Unlike(int id, ClaimsPrincipal currentUser)
        {
            _postRepository.Unlike(id, _userRepository.GetUser(currentUser));
        }

        public bool IsLiked(int id, ClaimsPrincipal currentUser)
        {
            if (!currentUser.Identity.IsAuthenticated) return false;
            return _postRepository.IsLiked(id, _userRepository.GetUser(currentUser));
        }
    }
}
