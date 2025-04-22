using Application.Common;
using Application.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Posts.Commands.SoftDelete
{
    internal class SoftDeletePostCommandHandler : IRequestHandler<SoftDeletePostCommand, ApiResponse<bool>>
    {
        public IUnitOfWork unitOfWork { get; }

        public SoftDeletePostCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<bool>> Handle(SoftDeletePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var postRepository = unitOfWork.Repository<Post>();
                var user = await postRepository.GetByIdAsync(request.Id);

                if (user == null)
                    return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, "Post no encontrado", false, false);

                await postRepository.SoftDeleteAsync(user);

                return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, "Post borrado correctamente", true, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.InternalServerError, ex.Message, false, false);
            }
        }
    }
}
