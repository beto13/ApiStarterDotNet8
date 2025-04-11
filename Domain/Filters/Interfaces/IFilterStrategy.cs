using System.Linq.Expressions;

namespace Domain.Filters.Interfaces
{
    public interface IFilterStrategy<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
