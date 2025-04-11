using Application.Common.Pagination;
using Application.Interfaces.Persistence;
using Domain.Entities;
using Domain.Filters.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public void Remove(T entity)
            => _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFilteredAndPagedAsync(
            Expression<Func<T, bool>> filter,
            PaginationParameters paginationParams)
        {
            var query = _dbSet.Where(filter)
                              .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                              .Take(paginationParams.PageSize);

            return await query.ToListAsync();
        }

        public virtual IQueryable<T> GetQueryable(
            Expression<Func<T, bool>>? filter = null,
            bool asNoTracking = false,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task<IEnumerable<T>> GetFilteredAndPagedAsync(
            Expression<Func<T, bool>> filter,
            PaginationParameters paginationParams,
            bool asNoTracking = false,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetQueryable(filter, asNoTracking, includeProperties)
                        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                        .Take(paginationParams.PageSize);

            return await query.ToListAsync();
        }

        public async Task<PagedResult<T>> GetPagedResultAsync(
            List<IFilterStrategy<T>> filters,
            PaginationParameters pagination,
            bool asNoTracking = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            foreach (var filter in filters)
                query = query.Where(filter.ToExpression());

            int totalCount = await query.CountAsync();

            var result = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Data = result,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<IEnumerable<T>> GetFilteredAndPagedAsync(
            Expression<Func<T, bool>> filter,
            PaginationParameters paginationParams,
            bool asNoTracking = false,
            params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

            // Agregar includes por nombre
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Aplicar filtro
            query = query.Where(filter);

            // Aplicar paginación
            query = query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);

            return await query.ToListAsync();
        }
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task SoftDeleteAsync(T entity)
        {
            if (entity is BaseEntity deletableEntity)
            {
                deletableEntity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
