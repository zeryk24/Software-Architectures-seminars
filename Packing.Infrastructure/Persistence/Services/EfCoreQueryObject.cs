using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Packing.Application.Services;
using RegistR.Attributes.Base;

namespace Packing.Infrastructure.Persistence.Services;

[Register(ServiceLifetime.Transient, typeof(IQueryObject<>))]
public class EfCoreQueryObject<TAggregate> : QueryObject<TAggregate> where TAggregate : class
{
    private readonly PackingDbContext _dbContext;

    public EfCoreQueryObject(PackingDbContext dbContext)
    {
        _dbContext = dbContext;
        _query = _dbContext.Set<TAggregate>().AsQueryable();
    }

    public override async Task<IEnumerable<TAggregate>> ExecuteAsync()
    {
        return await _query.ToListAsync();
    }
}