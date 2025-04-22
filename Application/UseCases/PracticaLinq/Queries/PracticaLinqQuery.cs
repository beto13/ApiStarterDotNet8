using Application.Common;
using MediatR;

namespace Application.UseCases.PracticaLinq.Queries
{
    public record PracticaLinqQuery() : IRequest<ApiResponse<bool>>;
}
