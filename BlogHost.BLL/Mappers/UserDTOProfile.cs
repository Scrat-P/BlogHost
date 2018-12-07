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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(opts => opts.Ignore());
            //.ForMember(x => x.Password, opt => opt.Ignore())
            //.ForMember(x => x.AllRoles, opt => opt.Ignore())
            //.ForMember(x => x.UserRoles, opt => opt.Ignore());
            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(opts => opts.Ignore());
        }
    }
}