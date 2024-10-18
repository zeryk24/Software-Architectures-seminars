using System.Reflection;
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
}