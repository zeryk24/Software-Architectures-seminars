using System.Reflection;
using Inventory.Domain.Common.Enums;
using Inventory.Domain.Goods;
using Inventory.Domain.Goods.ValueObjects;
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
        
        var goods = Goods.Create(GoodsId.Create(Guid.Parse("00000001-0000-0000-0000-000000000000")), "Svíčková", 10).Value;
        var goods2 = Goods.Create(GoodsId.Create(Guid.Parse("00000003-0000-0000-0000-000000000000")), "Guláš", 10).Value;

        context.Goods.Add(goods);
        context.Goods.Add(goods2);
        
        var orderItem = OrderItem.Create(
            OrderItemGoods.Create(goods.Id.Value), 
            OrderItemAmount.Create(3).Value,
            OrderItemPrice.Create(150, Currency.Czk).Value);

        var address = OrderAddress.Create(State.Czechia, "Brno", "63800", "Cejl 15");
        var address2 = OrderAddress.Create(State.Czechia, "Brno", "63800", "Cejl 10");

        var order = Order.Create(address, new[] { orderItem });
        var order2 = Order.Create(address2, new[] { orderItem,orderItem });

        context.Orders.Add(order);
        context.Orders.Add(order2);
            
        context.SaveChanges();
    }
}