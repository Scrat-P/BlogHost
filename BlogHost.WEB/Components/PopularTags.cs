using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.WEB.Models;
using BlogHost.BLL.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BlogHost.WEB.Models.MappingProfiles;

namespace BlogHost.WEB.Components
{
    public class PopularTags : ViewComponent
    {
        private readonly ITagService _tagService;

        public PopularTags(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.PopularTags = _tagService.GetPopularTags().ToVM();
            return View("_PopularTags");
        }
    }
}
