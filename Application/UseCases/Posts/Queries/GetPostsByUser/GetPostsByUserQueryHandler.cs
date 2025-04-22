using Application.Common;
using Application.Dtos.Posts;
using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Posts.Queries.GetPostsByUser
{
    internal class GetPostsByUserQueryHandler : IRequestHandler<GetPostsByUserQuery, ApiResponse<List<PostWithCommentsDto>>>
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public GetPostsByUserQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<PostWithCommentsDto>>> Handle(GetPostsByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var posts = await postRepository.GetAllByUserIdAsync(request.UserId);

                if (posts == null)
                    return new ApiResponse<List<PostWithCommentsDto>>(System.Net.HttpStatusCode.NoContent, "No se encontraron posts", true, []);

                var postsDto = mapper.Map<List<PostWithCommentsDto>>(posts);

                return new ApiResponse<List<PostWithCommentsDto>>(System.Net.HttpStatusCode.OK, "", true, postsDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<PostWithCommentsDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message, true, []);
            }
        }
    }
}
