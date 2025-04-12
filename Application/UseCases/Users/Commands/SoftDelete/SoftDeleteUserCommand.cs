using Application.Common;
using MediatR;

namespace Application.UseCases.Users.Commands.SoftDelete
{
    public record SoftDeleteUserCommand(Guid Id) : IRequest<ApiResponse<bool>>;
}

