using Application.Common;
using Application.Common.Pagination;
using Application.Dtos.User;
using Application.Filtering.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.UseCases.Users.Queries.GetFilteredUsersWithPost
{
    public class GetFilteredUsersWithPostsQueryHandler : IRequestHandler<GetFilteredUsersWithPostsQuery, ApiResponse<PagedResult<UserWithPostsDto>>>
    {
        private readonly IFilterService<User, UserFilterDto> filterService;
        private readonly IMapper mapper;

        public GetFilteredUsersWithPostsQueryHandler(IFilterService<User, UserFilterDto> filterService, IMapper mapper)
        {
            this.filterService = filterService;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<PagedResult<UserWithPostsDto>>> Handle(GetFilteredUsersWithPostsQuery request, CancellationToken cancellationToken)
        {
            var parameters = new PaginationParameters { PageNumber = request.PageNumber, PageSize = request.PageSize };

            var result = await filterService.Execute(request.UserFilter, parameters, u => u.Posts);
            var dtoList = mapper.Map<List<UserWithPostsDto>>(result.Data);

            var pagedDto = new PagedResult<UserWithPostsDto>
            {
                Data = dtoList,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };

            return new ApiResponse<PagedResult<UserWithPostsDto>>(HttpStatusCode.OK, "", true, pagedDto);
        }
    }
}
