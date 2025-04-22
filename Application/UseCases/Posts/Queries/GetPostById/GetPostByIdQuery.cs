using Application.Common;
using Application.Dtos.Posts;
using MediatR;

namespace Application.UseCases.Posts.Queries.GetPostById
{
    public record GetPostByIdQuery(Guid Id) : IRequest<ApiResponse<PostDto>>;
}
