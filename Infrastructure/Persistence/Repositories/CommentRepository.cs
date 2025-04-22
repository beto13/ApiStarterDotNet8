using Application.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public override async Task<Comment?> GetByIdAsync(Guid id)
            => await _context.Comments.FindAsync(id);

        public async Task<IEnumerable<Comment>> GetAllByPostIdAsync(Guid postId)
            => await _context.Comments
                             .Where(c => c.PostId == postId)
                             .ToListAsync();

        public override async Task AddAsync(Comment comment)
            => await _context.Comments.AddAsync(comment);

        public override void Remove(Comment comment)
            => _context.Comments.Remove(comment);
    }
}
