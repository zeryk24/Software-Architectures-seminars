using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Infrastructure.Repositories.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace FoodDelivery.BL.Tests;

public class HandlerFixture
{
    public Mock<IMapper> MapperMock { get; set; }
    public Mock<IUnitOfWorkProvider<IEFCoreUnitOfWork>> UnitOfWorkProviderMock { get; set; }
    public Mock<IEntityRepository<AddressEntity>> AddressRepositoryMock { get; set; }
    public Mock<IEntityRepository<RestaurantEntity>> RestaurantRepositoryMock { get; set; }
    public Mock<IEntityRepository<OrderItemEntity>> OrderItemRepositoryMock { get; set; }
    public Mock<IEntityRepository<OrderEntity>> OrderRepositoryMock { get; set; }
    public Mock<IEntityRepository<MealEntity>> MealRepositoryMock { get; set; }
    public Mock<IEntityRepository<FeedbackEntity>> FeedbackRepositoryMock { get; set; }
    public Mock<UserManager<UserEntity>> UserManagerMock { get; set; }
    public Mock<IEFCoreUnitOfWork> UnitOfWorkMock { get; set; }
    
    public HandlerFixture()
    {
        MapperMock = new Mock<IMapper>();

        UnitOfWorkProviderMock = new Mock<IUnitOfWorkProvider<IEFCoreUnitOfWork>>();
        UnitOfWorkMock = new Mock<IEFCoreUnitOfWork>();
        AddressRepositoryMock = new Mock<IEntityRepository<AddressEntity>>();
        FeedbackRepositoryMock = new Mock<IEntityRepository<FeedbackEntity>>();
        MealRepositoryMock = new Mock<IEntityRepository<MealEntity>>();
        OrderRepositoryMock = new Mock<IEntityRepository<OrderEntity>>();
        OrderItemRepositoryMock = new Mock<IEntityRepository<OrderItemEntity>>();
        RestaurantRepositoryMock = new Mock<IEntityRepository<RestaurantEntity>>();
        var store = new Mock<IUserStore<UserEntity>>();
        UserManagerMock = new Mock<UserManager<UserEntity>>(store.Object, null, null, null, null, null, null, null, null);
    }
}