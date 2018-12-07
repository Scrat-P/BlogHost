using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogHost.WEB.Models;
using BlogHost.DAL.Entities;
using BlogHost.DAL.Data;
using BlogHost.BLL.Mappers;
using BlogHost.WEB.Models.MappingProfiles;

namespace BlogHost.WEB.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Post> posts = _context.Posts
                .Include(element => element.Author)
                .Include(element => element.Blog)
                .Include(element => element.Likes)
                .Include(element => element.Comments)
                .Where(element => element.Blog.Id == 1);

            return View(posts.ToDTO().ToVM());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
