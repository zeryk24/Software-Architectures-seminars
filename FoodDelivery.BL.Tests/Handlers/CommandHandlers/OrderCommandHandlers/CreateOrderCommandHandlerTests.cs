using System.Security.Claims;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderCommandHandlers;

public class CreateOrderCommandHandlerTests : IClassFixture<OrderFixture>, IClassFixture<HandlerFixture>
{
    private readonly OrderFixture _orderFixture;
    private readonly HandlerFixture _handlerFixture;
    private readonly ClaimsPrincipal _user;

    public CreateOrderCommandHandlerTests(OrderFixture orderFixture, HandlerFixture handlerFixture)
    {
        _orderFixture = orderFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.OrderRepositoryMock.Setup(o => o.Insert(It.IsAny<OrderEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderRepository)
            .Returns(_handlerFixture.OrderRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderListModel>(It.IsAny<OrderEntity>()))
            .Returns(orderFixture.OrderListModel);

        _user = new ClaimsPrincipal();
        _handlerFixture.UserManagerMock.Setup(u => u.GetUserAsync(_user))
            .ReturnsAsync(new UserEntity
            {
                Id = 1,
            });
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new CreateOrderCommand((int)_orderFixture.OrderEntity.RestaurantId, _user);
        var handler = new CreateOrderCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object,
            _handlerFixture.UserManagerMock.Object);

        var expected = _orderFixture.OrderListModel;
        var actual = await handler.Handle(request, CancellationToken.None);
        
        Assert.Equal(expected, actual);
    }
}