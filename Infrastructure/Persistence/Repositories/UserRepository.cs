using Application.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public override async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Pruebas()
        {
            var allUsers = await _context.Users.ToListAsync();
            var allUsersQuantity = _context.Users.Count();
            var allActiveUsers = _context.Users.Where(u => u.DeletedAt == null).ToList();
            var usersName = _context.Users.Select(u => u.Name).ToList();

            var usersWithPost = _context.Users.Include(u=>u.Posts).ToList();
            var usersWithAtLeastOnePost = _context.Users.Include(u => u.Posts).Where(p => p.Posts.Any()).ToList();

            var userWithMostPosts = await _context.Users
                .OrderByDescending(u => u.Posts.Count)
                .FirstOrDefaultAsync();

            var postPerUser = await _context.Users
                .Select(u => new { 
                    name = u.Name, 
                    count = u.Posts.Count })
                .OrderByDescending(u => u.count)
                .ToListAsync();

            // 5. Título del post más reciente (por Id)
            var lastPostTitle = _context.Posts.OrderByDescending(p => p.CreatedAt).Select(p => p.Title).FirstOrDefault();

            // 10. Resumen plano: Usuario y Título del Post

            var resume = await _context.Users.SelectMany(u => u.Posts,(u,p) => new { u.Name, p.Title} ).ToListAsync();
        }
    }
}
