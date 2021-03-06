﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.DAL.RepositoryInterfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPostList();
        IEnumerable<Post> GetPostList(int blogId);
        IEnumerable<Post> GetPopularWeekPostsList();
        void Like(int id, ApplicationUser user);
        void Unlike(int id, ApplicationUser user);
        bool IsLiked(int id, ApplicationUser user);
        Post GetPost(int? id);
        void Create(Post post, Blog blog, ApplicationUser user, ICollection<string> tags);
        void Update(Post post, ICollection<string> tags);
        void Delete(int? id);
        void Save();
    }
}
