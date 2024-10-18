using System.Reflection;
using Inventory.Domain.Common.Enums;
using Inventory.Domain.Goods;
using Inventory.Domain.Order;
using Inventory.Domain.Order.OrderItem;
using Inventory.Domain.Order.OrderItem.ValueObjects;
using Inventory.Domain.Order.ValueObjects;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RegistR.Attributes.Extensions;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace Inventory;

public static class InventoryInstaller
{
    public static IServiceCollection InstallInventory(this IServiceCollection services, string inventoryConnectionString)
    {
        services.InstallRegisterAttribute(Assembly.GetExecutingAssembly());
        
        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseSqlite(inventoryConnectionString);
        });
        
        return services;
    }

    public static void SeedInventory(InventoryDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        var goods = Goods.Create("Svíčková", 10).Value;

        context.Goods.Add(goods);
        
        var orderItem = OrderItem.Create(
            OrderItemGoods.Create(goods.Id.Value), 
            OrderItemAmount.Create(3).Value,
            OrderItemPrice.Create(150, Currency.Czk).Value);

        var address = Address.Create(State.Czechia, "Brno", "63800", "Cejl 15");

        var order = Order.Create(address, new[] { orderItem });

        context.Orders.Add(order);
            
        context.SaveChanges();
    }
}