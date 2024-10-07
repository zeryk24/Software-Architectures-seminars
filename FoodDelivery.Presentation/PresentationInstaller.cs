using FoodDelivery.BL.Installers;
using FoodDelivery.DAL.Installers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Presentation;

public static class PresentationInstaller
{
    public static IServiceCollection InstallPresentation(this IServiceCollection services, string connectionString)
    {
        services.DalInstall(connectionString);
        services.BlInstall();
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        
        return services;
    }
}