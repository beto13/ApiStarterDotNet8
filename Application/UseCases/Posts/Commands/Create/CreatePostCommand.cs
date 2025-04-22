using Application.Common;
using Application.Dtos.Posts;
using MediatR;

namespace Application.UseCases.Posts.Commands.Create
{
    public record CreatePostCommand(PostsCreateDto Dto) : IRequest<ApiResponse<PostDto>>;
}
