using Application.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<Comment?> GetByIdAsync(Guid id)
            => await _context.Comments.FindAsync(id);

        public async Task<IEnumerable<Comment>> GetAllByPostIdAsync(Guid postId)
            => await _context.Comments
                             .Where(c => c.PostId == postId)
                             .ToListAsync();

        public async Task AddAsync(Comment comment)
            => await _context.Comments.AddAsync(comment);

        public void Remove(Comment comment)
            => _context.Comments.Remove(comment);
    }
}
