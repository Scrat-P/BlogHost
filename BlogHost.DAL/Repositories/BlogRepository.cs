using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Data;
using BlogHost.DAL.Entities;
using BlogHost.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogHost.DAL.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Blog> GetBlogList(ApplicationUser user)
        {
            return GetAllBlogList()
                .Where(author => author.Author.Id == user.Id);
        }

        public IEnumerable<Blog> GetAllBlogList()
        {
            return _context.Blogs
                .Include(author => author.Author);
        }

        public int? GetBlogId(string title)
        {
            Blog blog = _context.Blogs
                .FirstOrDefault(element => element.Title.ToLower() == title.ToLower());

            return blog?.Id;
        }

        public void Create(Blog blog, ApplicationUser user)
        {
            Blog databaseBlog = new Blog()
            {
                Author = user,
                Title = blog.Title,
                Description = blog.Description
            };

            _context.Blogs.Add(databaseBlog);
            Save();
        }

        public void Delete(int? id)
        {
            var blog = GetBlog(id);
            if (blog == null) return;

            _context.Blogs.Remove(blog);
            Save();
        }

        public void Update(Blog blog)
        {
            Blog databaseBlog = GetBlog(blog.Id);
            databaseBlog.Title = blog.Title;
            databaseBlog.Description = blog.Description;
            _context.Update(databaseBlog);
            Save();
        }

        public Blog GetBlog(int? id)
        {
            var blog = _context.Blogs
                .Include(element => element.Author)
                .FirstOrDefault(element => element.Id == id);
            return blog;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
