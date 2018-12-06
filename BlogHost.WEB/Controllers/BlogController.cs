using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IPostService _postService;

        public BlogController( 
            IBlogService blogService,
            IPostService postService)
        {
            _blogService = blogService;
            _postService = postService;
        }

        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            var blogsPerPage = _blogService.GetPageBlogs(page, pageSize, User, out int blogsCount).ToVM();

            PageViewModel pageViewModel = new PageViewModel(blogsCount, page, pageSize);
            IndexViewModel<BlogViewModel> viewModel = new IndexViewModel<BlogViewModel>
            {
                PageViewModel = pageViewModel,
                Items = blogsPerPage
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _blogService.Create(viewModel.ToDTO(), User);
                
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Show(int? id, int page = 1, int pageSize = 9)
        {
            if (_blogService.GetBlog(id, User) == null)
            {
                return NotFound();
            }

            IEnumerable<PostViewModel> postsPerPage = _postService.GetPagePosts(page, pageSize, (int)id, out int postsCount).ToVM();

            PageViewModel pageViewModel = new PageViewModel(postsCount, page, pageSize);
            IndexViewModel<PostViewModel> viewModel = new IndexViewModel<PostViewModel>
            {
                PageViewModel = pageViewModel,
                Items = postsPerPage
            };

            return View(viewModel);
        }

        public IActionResult Edit(int? id)
        {
            BlogViewModel blog = _blogService.GetBlog(id, User).ToVM();
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _blogService.Edit(viewModel.ToDTO());

                return RedirectToRoute(new { controller = "Blog", action = "Show", id = viewModel.Id });
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            _blogService.Delete(id, User);
            return RedirectToAction("Index");
        }
    }
}
