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
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<TagDTO> GetPopularTags()
        {
            return _tagRepository
                .GetTagsList()
                .ToDTO()
                .OrderBy(tag => tag.Posts.Count())
                .TakeLast(10);
        }
    }
}
