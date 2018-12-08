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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Comment comment)
        {
            _context.Comments.Add(comment);
            Save();
        }

        public void Delete(int? id)
        {
            var comment = GetComment(id);
            if (comment == null) return;

            _context.Comments.Remove(comment);
            Save();
        }

        public Comment GetComment(int? id)
        {
            var comment = _context.Comments
                .Include(element => element.Author)
                .FirstOrDefault(element => element.Id == id);
            return comment;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
