using Domain.Filters.Interfaces;
using System.Linq.Expressions;

namespace Domain.Filters.Strategies
{
    public class StartsWithFilterStrategy<T> : IFilterStrategy<T>
    {
        private readonly Expression<Func<T, string>> _propertySelector;
        private readonly string _value;

        public StartsWithFilterStrategy(Expression<Func<T, string>> propertySelector, string value)
        {
            _propertySelector = propertySelector;
            _value = value;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            var parameter = _propertySelector.Parameters[0];
            var body = Expression.Call(
                _propertySelector.Body,
                typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!,
                Expression.Constant(_value)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
