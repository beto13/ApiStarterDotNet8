using Application.Common;
using Application.Common.Pagination;
using Application.Dtos.Posts;
using MediatR;

namespace Application.UseCases.Posts.Queries.GetFilteredPosts
{
    public record GetFilteredPostsQuery(PostFilterDto Filter, int PageNumber = 1, int PageSize = 10) : IRequest<ApiResponse<PagedResult<PostDto>>>;
}
