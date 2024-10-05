using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.Repositories.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;

namespace FoodDelivery.DAL.EFCore.UnitOfWork;

public interface IEFCoreUnitOfWork : IUnitOfWork
{
    public IEntityRepository<AddressEntity> AddressRepository { get; }
    public IEntityRepository<FeedbackEntity> FeedbackRepository { get; }
    public IEntityRepository<MealEntity> MealRepository { get; }
    public IEntityRepository<OrderEntity> OrderRepository { get; }
    public IEntityRepository<OrderItemEntity> OrderItemRepository { get; }
    public IEntityRepository<RestaurantEntity> RestaurantRepository { get; }
    public IEntityRepository<UserEntity> UserRepository { get; }
}