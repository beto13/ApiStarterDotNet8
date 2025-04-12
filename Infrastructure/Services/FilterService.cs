using Application.Common.Pagination;
using Application.Interfaces.Persistence;
using Application.Interfaces.Services;
using Domain.Filters.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Services
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

        public async Task<PagedResult<T>> Execute(
            TFilterDto filterDto,
            PaginationParameters pagination,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var repository = unitOfWork.Repository<T>();

            var filters = filterStrategyFactory.CreateStrategies(filterDto);

            var result = await repository.GetPagedResultAsync(
                filters,
                pagination,
                asNoTracking: true,
                includeProperties
            );

            return result;
        }
    }
}
