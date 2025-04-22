using Application.Common;
using Application.Common.Pagination;
using Application.Dtos.Posts;
using Application.Filtering.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.UseCases.Posts.Queries.GetFilteredPosts
{
    internal class GetFilteredPostsQueryHandler : IRequestHandler<GetFilteredPostsQuery, ApiResponse<PagedResult<PostDto>>>
    {
        private readonly IFilterService<Post, PostFilterDto> filterService;
        private readonly IMapper mapper;

        public GetFilteredPostsQueryHandler(IFilterService<Post, PostFilterDto> filterService, IMapper mapper)
        {
            this.filterService = filterService;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<PagedResult<PostDto>>> Handle(GetFilteredPostsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var parameters = new PaginationParameters { PageNumber = request.PageNumber, PageSize = request.PageSize };

                var result = await filterService.Execute(request.Filter, parameters);

                var dtoList = mapper.Map<List<PostDto>>(result.Data);

                var pagedDto = new PagedResult<PostDto>
                {
                    Data = dtoList,
                    TotalCount = result.TotalCount,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize
                };

                return new ApiResponse<PagedResult<PostDto>>(HttpStatusCode.OK, "", true, pagedDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PagedResult<PostDto>>(HttpStatusCode.InternalServerError, ex.Message, true, null!);
            }
        }
    }
}
