using Application.Common;
using Application.Dtos.Posts;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Posts.Commands.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ApiResponse<PostDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<PostDto>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var postRepository = unitOfWork.Repository<Post>();

                var post = await postRepository.GetByIdAsync(request.dto.Id);

                if (post == null)
                    return new ApiResponse<PostDto>(System.Net.HttpStatusCode.BadRequest, "No se encontro post", false, null!);

                post!.Title = request.dto.Title;
                post.UpdatedAt = DateTime.Now;

                postRepository.Update(post);

                await unitOfWork.SaveChangesAsync();

                return new ApiResponse<PostDto>(System.Net.HttpStatusCode.OK, "Post actualizado correctamente", true, mapper.Map<PostDto>(post));
            }
            catch (Exception ex)
            {
                return new ApiResponse<PostDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message, false, null!);
            }
        }
    }
}
