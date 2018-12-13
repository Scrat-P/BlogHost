using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;
using BlogHost.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogHost.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Delete(string id)
        {
            _userManager.DeleteAsync(GetUser(id)).Wait();
        }

        public ApplicationUser GetUser(string id)
        {
            return _userManager.FindByIdAsync(id).Result;
        }

        public ApplicationUser GetUser(ClaimsPrincipal user)
        {
            return _userManager.GetUserAsync(user).Result;
        }

        public IList<string> GetUserRoles(string id)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;
            return _userManager.GetRolesAsync(user).Result;
        }

        public List<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public IdentityResult Update(ApplicationUser user, List<string> roles)
        {
            ApplicationUser databaseUser = _userManager.FindByIdAsync(user.Id).Result;
            databaseUser.Email = user.Email;
            databaseUser.UserName = user.UserName;

            var result = _userManager.UpdateAsync(databaseUser).Result;
            if (result.Succeeded)
            {
                var userRoles = _userManager.GetRolesAsync(databaseUser).Result;
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                _userManager.AddToRolesAsync(databaseUser, addedRoles).Wait();
                _userManager.RemoveFromRolesAsync(databaseUser, removedRoles).Wait();
            }

            return result;
        }

        public IdentityResult Create(ApplicationUser user, List<string> roles, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRolesAsync(user, roles).Wait();
            }

            return result;
        }

        public IEnumerable<ApplicationUser> GetUserList()
        {
            return _userManager.Users;
        }

        public byte[] GetProfilePicture(string id)
        {
            ApplicationUser user = GetUser(id);
            return user.ProfilePicture;
        }
    }
}
