using Application.Common;
using Application.Dtos.Posts;
using MediatR;

namespace Application.UseCases.Posts.Queries.GetPostsByUser
{
    public record GetPostsByUserQuery(Guid UserId) : IRequest<ApiResponse<List<PostWithCommentsDto>>>;
}
