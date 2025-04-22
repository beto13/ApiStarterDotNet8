using Application.Common;
using Application.Dtos.Posts;
using Application.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Posts.Queries.GetPostById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, ApiResponse<PostDto>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetPostByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<PostDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var postRepository = unitOfWork.Repository<Post>();

                var post = await postRepository.GetByIdAsync(request.Id);

                if (post == null)
                    return new ApiResponse<PostDto>(System.Net.HttpStatusCode.BadRequest, "No se encontro post", false, new PostDto());

                await postRepository.SoftDeleteAsync(post);

                return new ApiResponse<PostDto>(System.Net.HttpStatusCode.OK, "Post iliminado con exito", true, null!);

            }
            catch (Exception ex)
            {
                return new ApiResponse<PostDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message, true, null!);
            }
        }
    }
}
