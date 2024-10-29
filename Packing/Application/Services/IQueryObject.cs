using System.Linq.Expressions;

namespace Packing.Application.Services;

public interface IQueryObject<TAggregate> where TAggregate : class
{
    IQueryObject<TAggregate> Filter(Expression<Func<TAggregate, bool>> predicate);
    IQueryObject<TAggregate> OrderBy(Expression<Func<TAggregate, object>> selector, bool ascending = true);
    IQueryObject<TAggregate> Page(int page, int pageSize);

    Task<IEnumerable<TAggregate>> ExecuteAsync();
}