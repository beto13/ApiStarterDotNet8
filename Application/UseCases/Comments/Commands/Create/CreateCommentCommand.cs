using Application.Common;
using Application.Dtos.Comments;
using MediatR;

namespace Application.UseCases.Comments.Commands.Create
{
    public record CreateCommentCommand(CommentCreateDto dto) : IRequest<ApiResponse<CommentDto>>;
}
