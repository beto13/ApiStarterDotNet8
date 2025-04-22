using Application.Common;
using Application.Common.Pagination;
using Application.Dtos.User;
using Application.Filtering.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.UseCases.Users.Queries.GetFilteredUsers
{
    public class GetFilteredUsersQueryHandler : IRequestHandler<GetFilteredUsersQuery, ApiResponse<PagedResult<UserDto>>>
    {
        private readonly IFilterService<User, UserFilterDto> filterService;
        private readonly IMapper mapper;

        public GetFilteredUsersQueryHandler(IFilterService<User, UserFilterDto> filterService, IMapper mapper)
        {
            this.filterService = filterService;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<PagedResult<UserDto>>> Handle(GetFilteredUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var parameters = new PaginationParameters { PageNumber = request.PageNumber, PageSize = request.PageSize };

                var result = await filterService.Execute(request.UserFilter, parameters);

                var dtoList = mapper.Map<List<UserDto>>(result.Data);

                var pagedDto = new PagedResult<UserDto>
                {
                    Data = dtoList,
                    TotalCount = result.TotalCount,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize
                };

                return new ApiResponse<PagedResult<UserDto>>(HttpStatusCode.OK, "Usuarios recuperados exitosamente", true, pagedDto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PagedResult<UserDto>>(HttpStatusCode.InternalServerError, ex.Message, true, null!);
            }
        }
    }
}
