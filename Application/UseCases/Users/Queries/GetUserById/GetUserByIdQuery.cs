using Application.Common;
using Domain.Dtos.User;
using MediatR;

namespace Application.UseCases.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<ApiResponse<UserDto?>>;
}
