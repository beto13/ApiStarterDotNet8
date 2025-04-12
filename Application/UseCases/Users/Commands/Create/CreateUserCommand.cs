using Application.Common;
using Domain.Dtos.User;
using MediatR;

namespace Application.UseCases.Users.Commands.Create
{
    public record CreateUserCommand(UserCreateDto Dto) : IRequest<ApiResponse<UserDto>>;
}
