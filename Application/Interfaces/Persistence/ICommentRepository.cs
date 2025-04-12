using Domain.Entities;

namespace Application.Interfaces.Persistence
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetAllByPostIdAsync(Guid postId);
        Task AddAsync(Comment comment);
        void Remove(Comment comment);
    }
}
