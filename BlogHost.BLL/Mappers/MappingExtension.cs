using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BlogHost.BLL.DTO;
using BlogHost.DAL.Entities;

namespace BlogHost.BLL.Mappers
{
    public static class MappingExtension
    {
        public static Blog ToEntity(this BlogDTO dto) => Mapper.Map<Blog>(dto);
        public static BlogDTO ToDTO(this Blog entity) => Mapper.Map<BlogDTO>(entity);
        public static IEnumerable<Blog> ToEntity(this IEnumerable<BlogDTO> dto)
            => Mapper.Map<IEnumerable<Blog>>(dto);
        public static IEnumerable<BlogDTO> ToDTO(this IEnumerable<Blog> entity)
            => Mapper.Map<IEnumerable<BlogDTO>>(entity);


        public static Post ToEntity(this PostDTO dto) => Mapper.Map<Post>(dto);
        public static PostDTO ToDTO(this Post entity) => Mapper.Map<PostDTO>(entity);
        public static IEnumerable<Post> ToEntity(this IEnumerable<PostDTO> dto)
            => Mapper.Map<IEnumerable<Post>>(dto);
        public static IEnumerable<PostDTO> ToDTO(this IEnumerable<Post> entity)
            => Mapper.Map<IEnumerable<PostDTO>>(entity);


        public static Comment ToEntity(this CommentDTO dto) => Mapper.Map<Comment>(dto);
        public static CommentDTO ToDTO(this Comment entity) => Mapper.Map<CommentDTO>(entity);
        public static IEnumerable<Comment> ToEntity(this IEnumerable<CommentDTO> dto)
            => Mapper.Map<IEnumerable<Comment>>(dto);
        public static IEnumerable<CommentDTO> ToDTO(this IEnumerable<Comment> entity)
            => Mapper.Map<IEnumerable<CommentDTO>>(entity);


        public static Like ToEntity(this LikeDTO dto) => Mapper.Map<Like>(dto);
        public static LikeDTO ToDTO(this Like entity) => Mapper.Map<LikeDTO>(entity);
        public static IEnumerable<Like> ToEntity(this IEnumerable<LikeDTO> dto)
            => Mapper.Map<IEnumerable<Like>>(dto);
        public static IEnumerable<LikeDTO> ToDTO(this IEnumerable<Like> entity)
            => Mapper.Map<IEnumerable<LikeDTO>>(entity);


        public static Tag ToEntity(this TagDTO dto) => Mapper.Map<Tag>(dto);
        public static TagDTO ToDTO(this Tag entity) => Mapper.Map<TagDTO>(entity);
        public static IEnumerable<Tag> ToEntity(this IEnumerable<TagDTO> dto)
            => Mapper.Map<IEnumerable<Tag>>(dto);
        public static IEnumerable<TagDTO> ToDTO(this IEnumerable<Tag> entity)
            => Mapper.Map<IEnumerable<TagDTO>>(entity);
    }
}
