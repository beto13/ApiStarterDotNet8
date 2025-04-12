using Application.Common;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Dtos.User;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = request.Dto.Name,
                    Email = request.Dto.Email
                };

                var userRepository = unitOfWork.Repository<User>();
                await userRepository.AddAsync(user);
                await unitOfWork.SaveChangesAsync();

                var dto = mapper.Map<UserDto>(user);

                return new ApiResponse<UserDto>(System.Net.HttpStatusCode.Created, "Usuario creado correctamente", true, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message, false, new UserDto { });
            }
        }
    }
}
