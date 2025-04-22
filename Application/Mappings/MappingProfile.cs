using Application.Dtos.Comments;
using Application.Dtos.Posts;
using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserWithPostsDto>();

            CreateMap<Post, PostDto>();
            CreateMap<PostDto, PostDto>();
            CreateMap<Post, PostWithCommentsDto>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
         }
    }
}
