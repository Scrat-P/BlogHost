using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.WEB.Models;
using BlogHost.BLL.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BlogHost.WEB.Models.MappingProfiles;

namespace BlogHost.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int page = 1, int pageSize = 8)
        {
            var postsPerPage = _postService.GetPopularWeekPosts(page, pageSize, out int postsCount).ToVM();

            PageViewModel pageViewModel = new PageViewModel(postsCount, page, pageSize);
            IndexViewModel<PostViewModel> viewModel = new IndexViewModel<PostViewModel>
            {
                PageViewModel = pageViewModel,
                Items = postsPerPage
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
