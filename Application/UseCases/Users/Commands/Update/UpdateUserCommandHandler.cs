using Application.Common;
using Application.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepository = _unitOfWork.Repository<User>();
                var user = await userRepository.GetByIdAsync(request.UserDto.Id);

                if (user == null)
                    return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, "No se encontro post", false, false);

                user.Name = request.UserDto.Name;
                user.Email = request.UserDto.Email;

                userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, "No se encontro post", true, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.InternalServerError, ex.Message, false, false);
            }
        }
    }
}