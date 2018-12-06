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
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogService(UserManager<ApplicationUser> userManager, IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        private ApplicationUser GetUser(ClaimsPrincipal currentUser)
        {
            return _userManager.GetUserAsync(currentUser).Result;
        }

        private bool HasAccess(int? blogId, ClaimsPrincipal currentUser)
        {
            BlogDTO blog = _blogRepository.GetBlog(blogId).ToDTO();

            var user = GetUser(currentUser);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            return (user.Id == blog.Author.Id || userRoles.Contains("admin") || userRoles.Contains("moderator"));
        }

        public void Delete(int? id, ClaimsPrincipal currentUser)
        {
            if (HasAccess(id, currentUser))
            {
                _blogRepository.Delete(id);
            }
        }

        public BlogDTO GetBlog(int? id, ClaimsPrincipal currentUser)
        {
            if (!HasAccess(id, currentUser))
            {
                return null;
            }
            return _blogRepository.GetBlog(id).ToDTO();
        }

        public void Edit(BlogDTO blog)
        {
            BlogDTO databaseBlog = _blogRepository.GetBlog(blog.Id).ToDTO();
            databaseBlog.Title = blog.Title;
            databaseBlog.Description = blog.Description;
            _blogRepository.Update(databaseBlog.ToEntity());
        }

        public void Create(BlogDTO blog, ClaimsPrincipal currentUser)
        {
            BlogDTO databaseBlog = new BlogDTO()
            {
                Author = GetUser(currentUser),
                Title = blog.Title,
                Description = blog.Description
            };
            _blogRepository.Create(databaseBlog.ToEntity());
        }

        public IEnumerable<BlogDTO> GetPageBlogs(int page, int pageSize, ClaimsPrincipal currentUser, out int blogsCount)
        {
            var user = GetUser(currentUser);

            IEnumerable<BlogDTO> blogs = _blogRepository.GetBlogList(user).ToDTO();
            blogsCount = blogs.Count();
            IEnumerable<BlogDTO> blogsPerPage = blogs.Skip((page - 1) * pageSize).Take(pageSize);

            return blogsPerPage;
        }
    }
}
