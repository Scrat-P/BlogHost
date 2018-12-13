using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogHost.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogHost.DAL.RepositoryInterfaces
{
    public interface IUserRepository
    {
        void Delete(string id);
        ApplicationUser GetUser(string id);
        ApplicationUser GetUser(ClaimsPrincipal user);
        IdentityResult Update(ApplicationUser user, List<string> roles);
        IdentityResult Create(ApplicationUser user, List<string> roles, string password);
        IEnumerable<ApplicationUser> GetUserList();
        List<IdentityRole> GetAllRoles();
        IList<string> GetUserRoles(string id);
        byte[] GetProfilePicture(string id);
    }
}
