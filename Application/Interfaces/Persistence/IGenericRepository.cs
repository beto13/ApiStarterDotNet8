using Application.Common.Pagination;
using Domain.Filters.Interfaces;
using System.Linq.Expressions;

namespace Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetFilteredAndPagedAsync(Expression<Func<T, bool>> filter, PaginationParameters paginationParams);
        IQueryable<T> GetQueryable(Expression<Func<T, bool>>? filter = null, bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetFilteredAndPagedAsync(Expression<Func<T, bool>> filter, PaginationParameters paginationParams, bool asNoTracking = false, params Expression<Func<T, object>>[] includeProperties);
        Task<PagedResult<T>> GetPagedResultAsync(
            List<IFilterStrategy<T>> filters,
            PaginationParameters pagination,
            bool asNoTracking = false,
            params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task SoftDeleteAsync(T entity);
    }
}
