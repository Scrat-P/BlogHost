using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BlogHost.WEB.Models.MappingProfiles;
using BlogHost.BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using BlogHost.WEB.Models;

namespace BlogHost.WEB.Hubs
{
    [Authorize]
    public class CommentHub : Hub
    {
        private readonly ICommentService _commentService;

        public CommentHub(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task Add(int postId, string text)
        {
            var comment = new CommentViewModel { Text = text };
            _commentService.Create(comment.ToDTO(), Context.User, postId);
            await Clients.All.SendAsync("Add", postId, text, Context.User.Identity.Name);
        }
    }
}
