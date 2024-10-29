namespace Packing.Application.Services;

public interface IRepository<TAggregate> where TAggregate : class
{
    Task<TAggregate> InsertAsync(TAggregate entity);
    Task InsertRangeAsync(List<TAggregate> entity);
    void Update(TAggregate entity);
    Task<bool> RemoveAsync(object id);
    Task CommitAsync();
}