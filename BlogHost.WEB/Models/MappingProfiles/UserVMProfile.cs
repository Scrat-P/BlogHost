using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogHost.BLL.DTO;
using BlogHost.WEB.Models;

namespace BlogHost.WEB.Models.MappingProfiles
{
    public class UserVMProfile : Profile
    {
        public UserVMProfile()
        {
            CreateMap<UserViewModel, UserDTO>()
                .ReverseMap()
                .PreserveReferences();
        }
    }
}