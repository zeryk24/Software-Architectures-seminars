using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace Inventory;

public static class InventoryInstaller
{
    public static IServiceCollection InstallInventory(this IServiceCollection services, string inventoryConnectionString)
    {
        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseSqlite(inventoryConnectionString);
        });
        
        return services;
    }
}