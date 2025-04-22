using Application.Common;
using Application.Dtos.User;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto?>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<UserDto?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await unitOfWork.Repository<User>().GetByIdAsync(request.Id);

                if (user == null)
                    return new ApiResponse<UserDto?>(System.Net.HttpStatusCode.NotFound, "No se encontraron registros", false, null);

                var dto = mapper.Map<UserDto>(user);

                return new ApiResponse<UserDto?>(System.Net.HttpStatusCode.OK, string.Empty, true, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDto?>(System.Net.HttpStatusCode.InternalServerError, ex.Message, false, null);
            }
        }
    }
}
