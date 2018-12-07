using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Claims;
using BlogHost.BLL.DTO;
using BlogHost.DAL.Entities;
using BlogHost.BLL.ServiceInterfaces;
using BlogHost.DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using BlogHost.BLL.Mappers;

namespace BlogHost.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Delete(string id)
        {
            _userManager.DeleteAsync(GetUser(id).ToEntity()).Wait();
        }

        public UserDTO GetUser(string id)
        {
            if (id == null)
            {
                return new UserDTO() { AllRoles = _roleManager.Roles.ToList() };
            }
            else
            {
                UserDTO user = _userManager.FindByIdAsync(id).Result.ToDTO();
                user.UserRoles = _userManager.GetRolesAsync(user.ToEntity()).Result;
                user.AllRoles = _roleManager.Roles.ToList();
                return user;
            }
        }

        public IdentityResult Edit(UserDTO user, List<string> roles)
        {
            UserDTO databaseUser = GetUser(user.Id);
            databaseUser.Email = user.Email;
            databaseUser.UserName = user.UserName;

            var result = _userManager.UpdateAsync(databaseUser.ToEntity()).Result;
            if (result.Succeeded)
            {
                var userRoles = databaseUser.UserRoles;
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                _userManager.AddToRolesAsync(databaseUser.ToEntity(), addedRoles).Wait();
                _userManager.RemoveFromRolesAsync(databaseUser.ToEntity(), removedRoles).Wait();
            }

            return result;
        }

        public IdentityResult Create(UserDTO user, List<string> roles)
        {
            var result = _userManager.CreateAsync(user.ToEntity(), user.Password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRolesAsync(user.ToEntity(), roles).Wait();
            }

            return result;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _userManager.Users.ToDTO();
        }
    }
}
