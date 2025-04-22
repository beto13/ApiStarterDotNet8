using Domain.Entities;

namespace Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task Pruebas();
    }
}
