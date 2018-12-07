using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogHost.BLL.DTO;
using BlogHost.DAL.Entities;

namespace BlogHost.BLL.Mappers
{
    public class UserDTOProfile : Profile
    {
        public UserDTOProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ReverseMap()
                .PreserveReferences();
        }
    }
}