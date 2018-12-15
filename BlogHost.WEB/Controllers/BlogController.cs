using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.WEB.Models;
using BlogHost.BLL.DTO;
using BlogHost.WEB.Filters;
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

        private IndexViewModel<T> GetPageModel<T>(int itemsCount, int pageNumber, int pageSize, IEnumerable<T> itemsPerPage)
        {
            PageViewModel pageViewModel = new PageViewModel(itemsCount, pageNumber, pageSize);
            IndexViewModel<T> viewModel = new IndexViewModel<T>
            {
                PageViewModel = pageViewModel,
                Items = itemsPerPage
            };

            return viewModel;
        }

        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            var blogsPerPage = _blogService.GetPageBlogs(page, pageSize, User, out int blogsCount).ToVM();

            return View(GetPageModel(blogsCount, page, pageSize, blogsPerPage));
        }

        public IActionResult Create() => View();

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

        public IActionResult Search(string title = null, string author = null, int page = 1, int pageSize = 3)
        {
            var blogsPerPage = _blogService.Search(title, author, page, pageSize, out int blogsCount).ToVM();
            
            return View(GetPageModel(blogsCount, page, pageSize, blogsPerPage));
        }

        [AllowAnonymous]
        [TypeFilter(typeof(AliasRedirectActionFilter))]
        public IActionResult Show(int? id, int page = 1, int pageSize = 9)
        {
            if (_blogService.GetBlog(id, User, false) == null)
            {
                return NotFound();
            }

            var postsPerPage = _postService.GetPagePosts(page, pageSize, (int)id, out int postsCount).ToVM();

            return View(GetPageModel(postsCount, page, pageSize, postsPerPage));
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
