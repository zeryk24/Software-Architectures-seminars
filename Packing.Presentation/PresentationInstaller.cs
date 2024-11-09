using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RegistR.Attributes.Extensions;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace Packing.Presentation;

public static class PresentationInstaller
{
    public static IServiceCollection InstallPresentation(this IServiceCollection services)
    {
        services.InstallRegisterAttribute(Assembly.GetExecutingAssembly());
        
        return services;
    }
}