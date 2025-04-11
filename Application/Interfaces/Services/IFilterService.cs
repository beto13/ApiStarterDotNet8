using Application.Common.Pagination;
using System.Linq.Expressions;

namespace Application.Interfaces.Services
{
    public interface IFilterService<T, TFilterDto> where T : class
    {
        Task<PagedResult<T>> Execute(
            TFilterDto filterDto,
            PaginationParameters pagination,
            params Expression<Func<T, object>>[] includeProperties);
    }
}
