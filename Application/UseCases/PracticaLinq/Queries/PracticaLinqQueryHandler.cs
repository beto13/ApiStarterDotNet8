using Application.Common;
using Application.Interfaces.Persistence;
using MediatR;

namespace Application.UseCases.PracticaLinq.Queries
{
    internal class PracticaLinqQueryHandler : IRequestHandler<PracticaLinqQuery, ApiResponse<bool>>
    {
        private readonly IUserRepository userRepository;

        public PracticaLinqQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ApiResponse<bool>> Handle(PracticaLinqQuery request, CancellationToken cancellationToken)
        {

            //var allUsers = await userRepository.GetAllAsync();
            //var allUsersQuantity = allUsers.Count();
            //var allActiveUsers = allUsers.Where(u => u.DeletedAt == null); 
            //var usersName = allUsers.Select(u=>u.Name).ToList();

            await userRepository.Pruebas();

            throw new NotImplementedException();
        }
    }
}
