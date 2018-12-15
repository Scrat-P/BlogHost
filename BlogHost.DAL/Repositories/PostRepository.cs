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
 

        public IEnumerable<Post> GetPopularWeekPostsList()
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-7);

            return GetAllPostList()
                .Where(date => date.Created >= startDate && date.Created <= endDate);
        }

        public IEnumerable<Post> GetPostList(int blogId)
        {
            return GetAllPostList()
                .Where(element => element.Blog.Id == blogId);
        }

        public IEnumerable<Post> GetAllPostList()
        {
            return _context.Posts
                .Include(element => element.Author)
                .Include(element => element.Blog)
                .Include(tag => tag.Tags)
                .ThenInclude(tag => tag.Tag);
        }

        private Post AddTags(Post post, List<string> tags)
        {
            List<PostTag> tagsCollection = new List<PostTag>();
            foreach (string tag in tags)
            {
                Tag databaseTag = _context.Tags
                    .FirstOrDefault(element => element.Name == tag);
                if (databaseTag == null) databaseTag = new Tag { Name = tag };
                tagsCollection.Add(new PostTag() { Tag = databaseTag, Post = post });
            }

            post.Tags = tagsCollection;
            return post;
        }

        public void Create(Post post, Blog blog, ApplicationUser user, ICollection<string> tags)
        {
            var currentTime = DateTime.Now;
            Post databasePost = new Post()
            {
                Title = post.Title,
                Text = post.Text,
                Blog = blog,
                Author = user,
                Created = currentTime,
                LastUpdated = currentTime,
                ProfilePicture = post.ProfilePicture
            };

            databasePost = AddTags(databasePost, tags.ToList());

            var tagsCollection = databasePost.Tags;
            _context.Posts.Add(databasePost);
            _context.AddRange(tagsCollection);
            Save();
        }

        public void Delete(int? id)
        {
            var post = GetPost(id);
            if (post == null) return;

            _context.Posts.Remove(post);
            Save();
        }

        private Post DeleteTags(Post post, List<string> tags)
        {
            foreach (string tag in tags)
            {
                PostTag databasePostTag = _context.PostTags
                    .Include(element => element.Tag)
                    .ThenInclude(element => element.Posts)
                    .FirstOrDefault(element => element.Tag.Name == tag && element.Post.Id == post.Id);

                post.Tags.Remove(databasePostTag);
                if (databasePostTag.Tag.Posts.Count() == 1)
                {
                    _context.Tags.Remove(databasePostTag.Tag);
                }
                _context.PostTags.Remove(databasePostTag);
            }

            Save();
            return post;
        }

        public void Update(Post post, ICollection<string> tags)
        {
            Post databasePost = GetPost(post.Id);
            databasePost.Title = post.Title;
            databasePost.Text = post.Text;
            databasePost.LastUpdated = DateTime.Now;
            databasePost.ProfilePicture = post.ProfilePicture;

            List<string> postTags = databasePost.Tags.Select(tag => tag.Tag.Name).ToList();
            databasePost = DeleteTags(databasePost, postTags);
            databasePost = AddTags(databasePost, tags.ToList());

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
                .ThenInclude(tag => tag.Tag)
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
