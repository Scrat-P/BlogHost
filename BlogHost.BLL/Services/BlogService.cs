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
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;

        public BlogService(IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        private UserDTO GetUser(ClaimsPrincipal currentUser)
        {
            return _userRepository.GetUser(currentUser).ToDTO();
        }

        public bool HasAccess(int? blogId, ClaimsPrincipal currentUser)
        {
            BlogDTO blog = _blogRepository.GetBlog(blogId).ToDTO();

            var user = GetUser(currentUser);
            var userRoles = _userRepository.GetUserRoles(user.Id);

            return (user.Id == blog.Author.Id || userRoles.Contains("admin") || userRoles.Contains("moderator"));
        }

        public void Delete(int? id, ClaimsPrincipal currentUser)
        {
            if (HasAccess(id, currentUser))
            {
                _blogRepository.Delete(id);
            }
        }

        public BlogDTO GetBlog(int? id, ClaimsPrincipal currentUser, bool checkAccess = true)
        {
            if (checkAccess && !HasAccess(id, currentUser))
            {
                return null;
            }
            return _blogRepository.GetBlog(id).ToDTO();
        }

        public int? GetBlogId(string title)
        {
            return _blogRepository.GetBlogId(title);
        }

        public void Edit(BlogDTO blog)
        {
            _blogRepository.Update(blog.ToEntity());
        }

        public void Create(BlogDTO blog, ClaimsPrincipal currentUser)
        {
            _blogRepository.Create(blog.ToEntity(), GetUser(currentUser).ToEntity());
        }

        public IEnumerable<BlogDTO> GetPageBlogs(int page, int pageSize, ClaimsPrincipal currentUser, out int blogsCount)
        {
            var user = GetUser(currentUser);

            IEnumerable<BlogDTO> blogs = _blogRepository.GetBlogList(user.ToEntity()).ToDTO();
            blogsCount = blogs.Count();
            IEnumerable<BlogDTO> blogsPerPage = blogs.Skip((page - 1) * pageSize).Take(pageSize);

            return blogsPerPage;
        }
    }
}
