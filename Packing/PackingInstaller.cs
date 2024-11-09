using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Packing.Infrastructure.Persistence;
using RegistR.Attributes.Extensions;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace Packing;

public static class PackingInstaller
{
    public static IServiceCollection InstallPacking(this IServiceCollection services, string packingConnectionString)
    {
        services.InstallRegisterAttribute(Assembly.GetExecutingAssembly());
        
        services.AddDbContext<PackingDbContext>(options =>
        {
            options.UseSqlite(packingConnectionString);
        });
        
        return services;
    }
}