using Application.Common;
using Application.Common.Pagination;
using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.Users.Queries.GetFilteredUsers
{
    public record GetFilteredUsersQuery(UserFilterDto UserFilter, int PageNumber=1, int PageSize = 10) : IRequest<ApiResponse<PagedResult<UserDto>>>;
}
