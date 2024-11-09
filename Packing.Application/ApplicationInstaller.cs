using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RegistR.Attributes.Extensions;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace Packing.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection InstallApplication(this IServiceCollection services)
    {
        services.InstallRegisterAttribute(Assembly.GetExecutingAssembly());
        
        return services;
    }
}