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
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Delete(string id)
        {
            _userRepository.Delete(id);
        }

        public UserDTO GetUser(string id)
        {
            if (id == null)
            {
                return new UserDTO() { AllRoles = _userRepository.GetAllRoles() };
            }
            else
            {
                UserDTO user = _userRepository.GetUser(id).ToDTO();
                user.UserRoles = _userRepository.GetUserRoles(id);
                user.AllRoles = _userRepository.GetAllRoles();
                return user;
            }
        }

        public IdentityResult Edit(UserDTO user, List<string> roles)
        {
            return _userRepository.Update(user.ToEntity(), roles);
        }

        public IdentityResult Create(UserDTO user, List<string> roles)
        {
            return _userRepository.Create(user.ToEntity(), roles, user.Password);
        }

        public byte[] GetProfilePicture(string id)
        {
            if (id == null)
            {
                return null;
            }
            return _userRepository.GetProfilePicture(id);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _userRepository.GetUserList().ToDTO();
        }
    }
}
