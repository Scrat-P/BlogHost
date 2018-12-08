using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.WEB.Models;
using BlogHost.DAL.Entities;
using BlogHost.DAL.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using BlogHost.WEB.Models.MappingProfiles;

namespace BlogHost.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int postId, string text)
        {
            CommentViewModel comment = new CommentViewModel() { Text = text };
            _commentService.Create(comment.ToDTO(), User, postId);
            return RedirectToAction("Show", "Post", new { id = postId });
        }
    }
}
