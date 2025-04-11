using Domain.Filters.Interfaces;
using System.Linq.Expressions;

namespace Domain.Filters.Strategies
{
    public class DateRangeFilterStrategy<T> : IFilterStrategy<T>
    {
        private readonly Expression<Func<T, DateTime>> _propertySelector;
        private readonly DateTime _from;
        private readonly DateTime _to;

        public DateRangeFilterStrategy(Expression<Func<T, DateTime>> propertySelector, DateTime from, DateTime to)
        {
            _propertySelector = propertySelector;
            _from = from;
            _to = to;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            var param = _propertySelector.Parameters[0];
            var body = Expression.AndAlso(
                Expression.GreaterThanOrEqual(_propertySelector.Body, Expression.Constant(_from)),
                Expression.LessThanOrEqual(_propertySelector.Body, Expression.Constant(_to))
            );
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
