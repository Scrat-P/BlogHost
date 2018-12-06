using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.DAL.RepositoryInterfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPostList(int blogId);
        Post GetPost(int? id);
        void Create(Post post);
        void Update(Post post);
        void Delete(int? id);
        void Save();
    }
}
