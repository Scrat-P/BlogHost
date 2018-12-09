using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BlogHost.WEB.Models.MappingProfiles;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.WEB.Models;

namespace BlogHost.WEB.Hubs
{
    public class LikeHub : Hub
    {
        private readonly IPostService _postService;

        public LikeHub(IPostService postService)
        {
            _postService = postService;
        }

        public async Task Like(int id, bool isLiked)
        {
            if (!Context.User.Identity.IsAuthenticated) return;
            if (isLiked)
            {
                _postService.Unlike(id, Context.User);
            }
            else
            {
                _postService.Like(id, Context.User);
            }
            await Clients.All.SendAsync("Like", id, isLiked);
        }
    }
}
