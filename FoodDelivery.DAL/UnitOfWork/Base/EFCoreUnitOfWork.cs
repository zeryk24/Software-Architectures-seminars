using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.Repositories.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.Repositories.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;
using FoodDelivery.Shared.Models.AddressModels;

namespace FoodDelivery.DAL.EFCore.UnitOfWork.Base;

public class EFCoreUnitOfWork : IEFCoreUnitOfWork
{
    protected readonly ApplicationDbContext _dbContext;

    public EFCoreUnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        AddressRepository = new EFCoreEntityRepository<AddressEntity>(_dbContext);
        FeedbackRepository = new EFCoreEntityRepository<FeedbackEntity>(_dbContext);
        MealRepository = new EFCoreEntityRepository<MealEntity>(_dbContext);
        OrderRepository = new EFCoreEntityRepository<OrderEntity>(_dbContext);
        OrderItemRepository = new EFCoreEntityRepository<OrderItemEntity>(_dbContext);
        RestaurantRepository = new EFCoreEntityRepository<RestaurantEntity>(_dbContext);
        UserRepository = new EFCoreEntityRepository<UserEntity>(_dbContext);
    }

    public IEntityRepository<AddressEntity> AddressRepository { get; }
    public IEntityRepository<FeedbackEntity> FeedbackRepository { get; }
    public IEntityRepository<MealEntity> MealRepository { get; }
    public IEntityRepository<OrderEntity> OrderRepository { get; }
    public IEntityRepository<OrderItemEntity> OrderItemRepository { get; }
    public IEntityRepository<RestaurantEntity> RestaurantRepository { get; }
    public IEntityRepository<UserEntity> UserRepository { get; }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}