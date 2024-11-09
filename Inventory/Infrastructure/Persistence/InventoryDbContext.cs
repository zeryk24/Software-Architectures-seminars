using System.Reflection;
using Inventory.Domain.Common;
using Inventory.Domain.Goods;
using Inventory.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Wolverine;

namespace Inventory.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    private readonly IMessageBus _sender;
    public InventoryDbContext() { }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options, IMessageBus sender) : base(options)
    {
        _sender = sender;
    }

    public DbSet<Goods> Goods { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishEventsAsync();
        
        return result;
    }
    
    private async Task PublishEventsAsync()
    {
        var domainEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e =>
            {
                var events = e.DomainEvents;
                
                e.ClearEvents();
                
                return events;
            }).ToList();

        //TODO: handle errors
        foreach (var domainEvent in domainEvents)
        {
            await _sender.PublishAsync(domainEvent);

            var integrationEvent = domainEvent.MapToIntegrationEvent();
            if (integrationEvent is not null)
                await _sender.PublishAsync(integrationEvent); //TODO: handle outbox
        }
    }
}