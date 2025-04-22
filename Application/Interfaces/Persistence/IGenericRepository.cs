using Application.Common.Pagination;
using Application.Filtering.Interfaces;
using System.Linq.Expressions;

namespace Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T entity);

        Task SoftDeleteAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> expression);
       
        Task<IEnumerable<T>> GetFilteredAndPagedAsync(
            Expression<Func<T, bool>> filter, 
            PaginationParameters paginationParams, 
            bool asNoTracking = false, 
            params Expression<Func<T, object>>[] includeProperties);
        
        Task<PagedResult<T>> GetFilteredAndPagedAsync(
            List<IFilterStrategy<T>> filters,
            PaginationParameters pagination,
            bool asNoTracking = false,
            params Expression<Func<T, object>>[] includes);
    }
}
