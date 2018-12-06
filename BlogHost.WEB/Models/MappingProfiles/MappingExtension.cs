using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogHost.BLL.DTO;
using BlogHost.WEB.Models;

namespace BlogHost.WEB.Models.MappingProfiles
{
    public static class MappingExtension
    {
        public static BlogViewModel ToVM(this BlogDTO dto) => Mapper.Map<BlogViewModel>(dto);
        public static BlogDTO ToDTO(this BlogViewModel vm) => Mapper.Map<BlogDTO>(vm);
        public static IEnumerable<BlogViewModel> ToVM(this IEnumerable<BlogDTO> dto)
            => Mapper.Map<IEnumerable<BlogViewModel>>(dto);
        public static IEnumerable<BlogDTO> ToDTO(this IEnumerable<BlogViewModel> vm)
            => Mapper.Map<IEnumerable<BlogDTO>>(vm);


        public static PostViewModel ToVM(this PostDTO dto) => Mapper.Map<PostViewModel>(dto);
        public static PostDTO ToDTO(this PostViewModel vm) => Mapper.Map<PostDTO>(vm);
        public static IEnumerable<PostViewModel> ToVM(this IEnumerable<PostDTO> dto)
            => Mapper.Map<IEnumerable<PostViewModel>>(dto);
        public static IEnumerable<PostDTO> ToDTO(this IEnumerable<PostViewModel> vm)
            => Mapper.Map<IEnumerable<PostDTO>>(vm);


        public static CommentViewModel ToVM(this CommentDTO dto) => Mapper.Map<CommentViewModel>(dto);
        public static CommentDTO ToDTO(this CommentViewModel vm) => Mapper.Map<CommentDTO>(vm);
        public static IEnumerable<CommentViewModel> ToVM(this IEnumerable<CommentDTO> dto)
            => Mapper.Map<IEnumerable<CommentViewModel>>(dto);
        public static IEnumerable<CommentDTO> ToDTO(this IEnumerable<CommentViewModel> vm)
            => Mapper.Map<IEnumerable<CommentDTO>>(vm);


        public static LikeViewModel ToVM(this LikeDTO dto) => Mapper.Map<LikeViewModel>(dto);
        public static LikeDTO ToDTO(this LikeViewModel vm) => Mapper.Map<LikeDTO>(vm);
        public static IEnumerable<LikeViewModel> ToVM(this IEnumerable<LikeDTO> dto)
            => Mapper.Map<IEnumerable<LikeViewModel>>(dto);
        public static IEnumerable<LikeDTO> ToDTO(this IEnumerable<LikeViewModel> vm)
            => Mapper.Map<IEnumerable<LikeDTO>>(vm);


        public static TagViewModel ToVM(this TagDTO dto) => Mapper.Map<TagViewModel>(dto);
        public static TagDTO ToDTO(this TagViewModel vm) => Mapper.Map<TagDTO>(vm);
        public static IEnumerable<TagViewModel> ToVM(this IEnumerable<TagDTO> dto)
            => Mapper.Map<IEnumerable<TagViewModel>>(dto);
        public static IEnumerable<TagDTO> ToDTO(this IEnumerable<TagViewModel> vm)
            => Mapper.Map<IEnumerable<TagDTO>>(vm);
    }
}
