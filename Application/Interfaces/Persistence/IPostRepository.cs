using Domain.Entities;

namespace Application.Interfaces.Persistence
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(Post post);
        void Remove(Post post);
    }
}
