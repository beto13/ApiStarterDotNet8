using Application.Common;
using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ApiResponse<UserDto?>>;
}
