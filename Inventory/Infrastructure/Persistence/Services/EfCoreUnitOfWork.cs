using Inventory.Features.Services;
using RegistR.Attributes.Base;

namespace Inventory.Infrastructure.Persistence.Services;

[Register<IUnitOfWork>]
public class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly InventoryDbContext _context;

    public EfCoreUnitOfWork(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}