using System.Reflection;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using RegistR.Attributes.Extensions;
using Wolverine.Attributes;

[assembly: WolverineModule]
namespace EventLog;

public static class EventLogInstaller
{
    public static IServiceCollection InstallInventory(this IServiceCollection services, string eventLogConncetionString)
    {
        services.InstallRegisterAttribute(Assembly.GetExecutingAssembly());

        var cn = Environment.GetEnvironmentVariable("ConnectionStrings__postgres");
        //var cns = Environment.GetEnvironmentVariables();
        
        services.AddMarten(
            options => options.Connection(cn!));
        
        return services;
    }
}