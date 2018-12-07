using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.WEB.Models.MappingProfiles;
using BlogHost.WEB.Models;

namespace CustomIdentityApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index() => View(_userService.GetUsers().ToVM());

        public IActionResult Create() => View(_userService.GetUser(null).ToVM());

        [HttpPost]
        public IActionResult Create(UserViewModel model, List<string> roles)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Create(model.ToDTO(), roles);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            
            return View(_userService.GetUser(null).ToVM());
        }

        public IActionResult Edit(string id)
        {
            UserViewModel user = _userService.GetUser(id).ToVM();
            if (user != null)
            {
                return View(user);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel model, List<string> roles)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Edit(model.ToDTO(), roles);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(_userService.GetUser(model.Id).ToVM());
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _userService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}