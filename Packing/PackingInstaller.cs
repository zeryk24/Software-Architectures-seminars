using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Packing.Application;
using Packing.Domain;
using Packing.Infrastructure;
using Packing.Infrastructure.Persistence;
using Packing.Presentation;
using RegistR.Attributes.Extensions;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace Packing;

public static class PackingInstaller
{
    public static IServiceCollection InstallPacking(this IServiceCollection services, string packingConnectionString)
    {
        services.InstallRegisterAttribute(Assembly.GetExecutingAssembly());

        services.InstallPresentation();
        services.InstallInfrastructure(packingConnectionString);
        services.InstallApplication();
        services.InstallDomain();
        
        return services;
    }

    public static void Configure(PackingDbContext? context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}