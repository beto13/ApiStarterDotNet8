using Application.Common;
using Application.Common.Pagination;
using Domain.Dtos.User;
using MediatR;

namespace Application.UseCases.Users.Queries.GetFilteredUsers
{
    public record GetFilteredUsersQuery(UserFilterDto UserFilter, int PageNumber, int PageSize) : IRequest<ApiResponse<PagedResult<UserDto>>>;
}
