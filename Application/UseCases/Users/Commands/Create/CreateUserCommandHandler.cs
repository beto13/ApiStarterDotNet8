using Application.Common;
using Application.Dtos.User;
using Application.Interfaces.Persistence;
using AutoMapper;
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
                var userRepository = unitOfWork.Repository<User>();

                var existingUser = await userRepository.FilterAsync(u => u.Email == request.Dto.Email);

                if (existingUser.Any())
                    return new ApiResponse<UserDto>(System.Net.HttpStatusCode.BadRequest, Messages.User.EmailAlreadyExists, true, new UserDto { });

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = request.Dto.Name,
                    Email = request.Dto.Email
                };

                await userRepository.AddAsync(user);
                await unitOfWork.SaveChangesAsync();

                var dto = mapper.Map<UserDto>(user);

                return new ApiResponse<UserDto>(System.Net.HttpStatusCode.Created, Messages.User.Created, true, dto);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDto>(System.Net.HttpStatusCode.InternalServerError, Messages.User.CreateError + ex.Message, false, new UserDto { });
            }
        }
    }
}
