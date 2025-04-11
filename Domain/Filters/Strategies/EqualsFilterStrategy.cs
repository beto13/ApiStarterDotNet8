using Domain.Filters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters.Strategies
{
    public class EqualsFilterStrategy<T, TProperty> : IFilterStrategy<T>
    {
        private readonly Expression<Func<T, TProperty>> _propertySelector;
        private readonly TProperty _value;

        public EqualsFilterStrategy(Expression<Func<T, TProperty>> propertySelector, TProperty value)
        {
            _propertySelector = propertySelector;
            _value = value;
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            var param = _propertySelector.Parameters[0];
            var body = Expression.Equal(_propertySelector.Body, Expression.Constant(_value));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }

}
