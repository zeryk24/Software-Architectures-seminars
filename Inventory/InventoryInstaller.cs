using Microsoft.Extensions.DependencyInjection;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace Inventory;

public static class InventoryInstaller
{
    public static IServiceCollection InstallInventory(this IServiceCollection services)
    {
        return services;
    }
}