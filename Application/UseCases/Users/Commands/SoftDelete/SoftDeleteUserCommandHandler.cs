using Application.Common;
using Application.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Commands.SoftDelete
{
    public class SoftDeleteUserCommandHandler : IRequestHandler<SoftDeleteUserCommand, ApiResponse<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public SoftDeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<bool>> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepository = unitOfWork.Repository<User>();
                var user = await userRepository.GetByIdAsync(request.Id);

                if (user == null)
                    return new ApiResponse<bool>(System.Net.HttpStatusCode.BadRequest, "Usuario no encontrado", false, false);

                await userRepository.SoftDeleteAsync(user);

                return new ApiResponse<bool>(System.Net.HttpStatusCode.OK, "Usuario borrado correctamente", true, true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(System.Net.HttpStatusCode.InternalServerError, ex.Message, false, false);
            }
        }
    }
}
