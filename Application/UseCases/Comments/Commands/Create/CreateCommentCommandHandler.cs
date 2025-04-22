using Application.Common;
using Application.Dtos.Comments;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.UseCases.Comments.Commands.Create
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ApiResponse<CommentDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CreateCommentCommandHandler> logger;
        private readonly IMapper mapper;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateCommentCommandHandler> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var commentRepository = unitOfWork.Repository<Comment>();

                var comment = new Comment { Id = new Guid(), Content = request.dto.Content, PostId = request.dto.PostId};

                await commentRepository.AddAsync(comment);
                await unitOfWork.SaveChangesAsync();

                var commentDto = mapper.Map<CommentDto>(comment);

                logger.LogInformation(Messages.Comment.Created);
                return new ApiResponse<CommentDto>(HttpStatusCode.Created, Messages.Comment.Created, true, commentDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Messages.Comment.CreateError);
                return new ApiResponse<CommentDto>(HttpStatusCode.InternalServerError, Messages.Comment.CreateError, true, null!);
            }
        }
    }
}
