using Application.Common;
using Application.Dtos.Posts;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Posts.Commands.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiResponse<PostDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<CreatePostCommandHandler> logger;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePostCommandHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<ApiResponse<PostDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var postRepository = unitOfWork.Repository<Post>();

                var post = new Post { Id = new Guid(), Title = request.Dto.Title, UserId = request.Dto.UserId };

                await postRepository.AddAsync(post);
                await unitOfWork.SaveChangesAsync();

                var postDto = mapper.Map<PostDto>(post);

                logger.LogInformation(Messages.Post.Created);
                return new ApiResponse<PostDto>(System.Net.HttpStatusCode.Created, Messages.Post.Created, true, postDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Messages.Post.CreateError);
                return new ApiResponse<PostDto>(System.Net.HttpStatusCode.InternalServerError, Messages.Post.CreateError + ex.Message, false, new PostDto());
            }
        }
    }
}
