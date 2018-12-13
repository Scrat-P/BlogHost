using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using BlogHost.BLL.DTO;
using Microsoft.AspNetCore.Identity;

namespace BlogHost.BLL.ServiceInterfaces
{
    public interface IUserService
    {
        void Delete(string id);
        UserDTO GetUser(string id);
        IdentityResult Edit(UserDTO user, List<string> roles);
        IdentityResult Create(UserDTO user, List<string> roles);
        IEnumerable<UserDTO> GetUsers();
        byte[] GetProfilePicture(string id);
    }
}
