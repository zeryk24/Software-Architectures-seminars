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

    public InventoryDbContext(DbContextOptions options, IMessageBus sender) : base(options)
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