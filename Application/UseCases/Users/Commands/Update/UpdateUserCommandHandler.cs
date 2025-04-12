using Application.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.Repository<User>();
            var user = await userRepository.GetByIdAsync(request.UserDto.Id);

            if (user == null)
                return false;

            user.Name = request.UserDto.Name;
            user.Email = request.UserDto.Email;

            userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}