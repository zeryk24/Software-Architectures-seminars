using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DAL.Infrastructure.QueryObjects.Base;

public abstract class QueryObject<TEntity> : IQueryObject<TEntity> where TEntity : class, new()
{
    protected IQueryable<TEntity> _query;

    public IQueryObject<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
    {
        _query = _query.Where(predicate);
        return this;
    }

    public IQueryObject<TEntity> Page(int page, int pageSize)
    {
        _query = _query.Skip((page - 1) * pageSize).Take(pageSize);
        return this;
    }

    public IQueryObject<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> selector, bool ascending = true)
    {
        _query = ascending switch
        {
            true => _query.OrderBy(selector),
            false => _query.OrderByDescending(selector)
        };
        return this;
    }

    public abstract Task<IEnumerable<TEntity>> ExecuteAsync();
}