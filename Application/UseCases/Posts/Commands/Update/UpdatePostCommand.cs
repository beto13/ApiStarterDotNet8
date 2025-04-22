using Application.Common;
using Application.Dtos.Posts;
using MediatR;

namespace Application.UseCases.Posts.Commands.Update
{
    public record UpdatePostCommand(PostDto dto) : IRequest<ApiResponse<PostDto>>;
}
