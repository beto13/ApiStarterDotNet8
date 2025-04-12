using Application.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Post>> GetAllByUserIdAsync(Guid userId)
            => await _context.Posts
                             .Where(p => p.UserId == userId)
                             .ToListAsync();
    }
}
