using Application.Common;
using MediatR;

namespace Application.UseCases.Posts.Commands.SoftDelete
{
    public record SoftDeletePostCommand(Guid Id) : IRequest<ApiResponse<bool>>;
}
