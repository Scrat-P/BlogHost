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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Like(int id, ApplicationUser user)
        {
            Post databasePost = GetPost(id);
            if (databasePost.Likes.Any(element => element.Author.Id == user.Id)) return;

            Like like = new Like()
            {
                Post = databasePost,
                Author = user
            };
            _context.Likes.Add(like);
            _context.Posts.Update(databasePost);
            Save();
        }

        public void Unlike(int id, ApplicationUser user)
        {
            Like like = _context.Likes.FirstOrDefault(element => element.Author.Id == user.Id);
            if (like == null) return;

            Post databasePost = GetPost(id);
            databasePost.Likes.Remove(like);
            _context.Likes.Remove(like);
            _context.Posts.Update(databasePost);
            Save();
        }

        public IEnumerable<Post> GetPostList(int blogId)
        {
            return _context.Posts
                .Include(element => element.Author)
                .Include(element => element.Blog)
                .Include(element => element.Likes)
                .Include(element => element.Comments)
                .Where(element => element.Blog.Id == blogId);
        }

        public void Create(Post post)
        {
            _context.Posts.Add(post);
            Save();
        }

        public void Delete(int? id)
        {
            var post = GetPost(id);
            if (post == null) return;

            _context.Posts.Remove(post);
            Save();
        }

        public void Update(Post post)
        {
            Post databasePost = GetPost(post.Id);
            databasePost.Title = post.Title;
            databasePost.Text = post.Text;
            databasePost.LastUpdated = DateTime.Now;
            _context.Update(databasePost);
            Save();
        }

        public bool IsLiked(int id, ApplicationUser user)
        {
            Post databasePost = GetPost(id);
            if (databasePost.Likes.Any(element => element.Author.Id == user.Id)) return true;
            return false;
        }

        public Post GetPost(int? id)
        {
            var post = _context.Posts
                .Include(user => user.Author)
                .Include(blog => blog.Blog)
                .Include(like => like.Likes)
                .Include(tag => tag.Tags)
                .Include(comment => comment.Comments)
                .ThenInclude(comment => comment.Author)
                .FirstOrDefault(element => element.Id == id);
            return post;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
