using Application.Common.Pagination;
using Application.Filtering.Interfaces;
using Application.Interfaces.Persistence;
using System.Linq.Expressions;

namespace Application.Services
{
    public class FilterService<T, TFilterDto> : IFilterService<T, TFilterDto> where T : class
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFilterStrategyFactory<T, TFilterDto> filterStrategyFactory;

        public FilterService(IUnitOfWork unitOfWork, IFilterStrategyFactory<T, TFilterDto> filterStrategyFactory)
        {
            this.unitOfWork = unitOfWork;
            this.filterStrategyFactory = filterStrategyFactory;
        }

        public virtual async Task<PagedResult<T>> Execute(
            TFilterDto filterDto,
            PaginationParameters pagination,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var repository = unitOfWork.Repository<T>();

            var filters = filterStrategyFactory.CreateStrategies(filterDto);

            var result = await repository.GetFilteredAndPagedAsync(
                filters,
                pagination,
                asNoTracking: true,
                includeProperties
            );

            return result;
        }
    }
}
