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
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetTagsList()
        {
            return _context.Tags
                .Include(element => element.Posts);
        }
    }
}
