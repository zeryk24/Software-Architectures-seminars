using System.Reflection;
using Inventory.Domain.Goods;
using Inventory.Domain.Order;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext() { }

    public InventoryDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Goods> Goods { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}