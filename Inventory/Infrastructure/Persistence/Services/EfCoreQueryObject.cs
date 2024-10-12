using Inventory.Features.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RegistR.Attributes.Base;

namespace Inventory.Infrastructure.Persistence.Services;

[Register(ServiceLifetime.Transient, typeof(IQueryObject<>))]
public class EfCoreQueryObject<TAggregate> : QueryObject<TAggregate> where TAggregate : class
{
    private readonly InventoryDbContext _dbContext;

    public EfCoreQueryObject(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
        _query = _dbContext.Set<TAggregate>().AsQueryable();
    }

    public override async Task<IEnumerable<TAggregate>> ExecuteAsync()
    {
        return await _query.ToListAsync();
    }
}