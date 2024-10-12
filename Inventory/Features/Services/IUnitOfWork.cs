namespace Inventory.Features.Services;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
}