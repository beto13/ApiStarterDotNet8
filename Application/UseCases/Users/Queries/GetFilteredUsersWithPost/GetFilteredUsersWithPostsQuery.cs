using Application.Common;
using Application.Common.Pagination;
using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.Users.Queries.GetFilteredUsersWithPost
{
    public record GetFilteredUsersWithPostsQuery(UserFilterDto UserFilter, int PageNumber, int PageSize) : IRequest<ApiResponse<PagedResult<UserWithPostsDto>>>;
}
