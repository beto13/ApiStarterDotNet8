using Application.Common;
using Application.Dtos.User;
using MediatR;

namespace Application.UseCases.Users.Commands.Update
{
    public record UpdateUserCommand(UserDto UserDto) : IRequest<ApiResponse<bool>>;
}