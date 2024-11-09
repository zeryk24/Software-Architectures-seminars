using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Packing.Domain.Common;
using Wolverine;

namespace Packing.Infrastructure.Persistence;

public class PackingDbContext : DbContext
{
    private readonly IMessageBus _sender;
    
    public PackingDbContext() { }

    public PackingDbContext(DbContextOptions<PackingDbContext> options, IMessageBus sender) : base(options)
    {
        _sender = sender;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents);
        
        var result = await base.SaveChangesAsync(cancellationToken);

        //TODO: handle errors
        foreach (var domainEvent in domainEvents)
        {
            await _sender.PublishAsync(domainEvent);
        }
        
        return result;
    }
}