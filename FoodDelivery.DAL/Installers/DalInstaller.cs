using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.DAL.Installers;

public static class DalInstaller
{
    public static IServiceCollection DalInstall(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        

        services.AddScoped<IUnitOfWorkProvider<IEFCoreUnitOfWork>, EFCoreUnitOfWorkProvider>((services) =>
        {
            return new EFCoreUnitOfWorkProvider(new Func<ApplicationDbContext>(() => services.GetRequiredService<ApplicationDbContext>()));
        });

        services.AddTransient<IGetAllMealFeedbacksQueryObject<FeedbackEntity>, GetAllMealFeedbacksQueryObject>();
        services.AddTransient<IGetAllRestaurantFeedbacksQueryObject<FeedbackEntity>, GetAllRestaurantFeedbacksQueryObject>();
        services.AddTransient<IGetAllRestaurantMealsQueryObject<MealEntity>, GetAllRestaurantMealsQueryObject>();
        services.AddTransient<IGetAllUserOrdersQueryObject<OrderEntity>, GetAllUserOrdersQueryObject>();
        services.AddTransient<IGetMealsByNameQueryObject<MealEntity>, GetMealsByNameQueryObject>();
        services.AddTransient<IGetMealsByTypeQueryObject<MealEntity>, GetMealsByTypeQueryObject>();
        services.AddTransient<IGetOrdersByRestaurantId<OrderEntity>, GetOrdersByRestaurantId>();
        services.AddTransient<IGetOrderItemsByOrderIdQueryObject<OrderItemEntity>, GetOrderItemsByOrderIdQueryObject>();

        services.AddTransient(typeof(IQueryObject<>), typeof(EFCoreQueryObject<>));

        return services;
    }
}
