using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;

namespace BlogHost.DAL.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        Comment GetComment(int? id);
        void Create(Comment comment);
        void Delete(int? id);
        void Save();
    }
}
