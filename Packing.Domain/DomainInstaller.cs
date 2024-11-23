using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RegistR.Attributes.Extensions;

namespace Packing.Domain;

public static class DomainInstaller
{
    public static IServiceCollection InstallDomain(this IServiceCollection services)
    {
        services.InstallRegisterAttribute(Assembly.GetExecutingAssembly());
        
        return services;
    }
}