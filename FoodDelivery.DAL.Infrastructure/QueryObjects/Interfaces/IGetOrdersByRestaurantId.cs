using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;

namespace FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;

public interface IGetOrdersByRestaurantId<TEntity> : IQueryObject<TEntity> where TEntity : class, new()
{
    IQueryObject<TEntity> UseFilter(int restaurantId);
}