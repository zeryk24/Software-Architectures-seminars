using System.Security.Claims;
using FluentAssertions;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;
using FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.OrderModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class CreateOrderItemCommandHandlerTests : IClassFixture<OrderItemFixture>, IClassFixture<HandlerFixture>
{
    private readonly OrderItemFixture _orderItemFixture;
    private readonly HandlerFixture _handlerFixture;
    private OrderItemEntity _resultBeforeMapping;
    private ClaimsPrincipal _user;

    public CreateOrderItemCommandHandlerTests(OrderItemFixture orderItemFixture, HandlerFixture handlerFixture)
    {
        _orderItemFixture = orderItemFixture;
        _handlerFixture = handlerFixture;

        _user = new ClaimsPrincipal();
        _handlerFixture.UserManagerMock.Setup(u => u.GetUserAsync(_user))
            .ReturnsAsync(new UserEntity
            {
                Id = 1,
            });

        _handlerFixture.MealRepositoryMock.Setup(m => m.GetByIdAsync(_orderItemFixture.OrderItemCreateModel.MealId))
            .ReturnsAsync(new MealEntity
            {
                Price = 10,
            });
        _handlerFixture.OrderItemRepositoryMock.Setup(o => o.Insert(It.IsAny<OrderItemEntity>()));
        _handlerFixture.OrderRepositoryMock.Setup(o => o.GetByIdAsync(_orderItemFixture.OrderItemCreateModel.OrderId))
            .ReturnsAsync(new OrderEntity{ UserId = 1,});
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderItemRepository)
            .Returns(_handlerFixture.OrderItemRepositoryMock.Object);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.MealRepository)
            .Returns(_handlerFixture.MealRepositoryMock.Object);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderRepository)
            .Returns(_handlerFixture.OrderRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        var entity = new OrderItemEntity
        {
            OrderId = _orderItemFixture.OrderItemCreateModel.OrderId,
            MealId = _orderItemFixture.OrderItemCreateModel.OrderId,
            Amount = _orderItemFixture.OrderItemCreateModel.Amount
        };
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderItemEntity>(_orderItemFixture.OrderItemCreateModel))
            .Returns(entity);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderItemDetailModel>(It.IsAny<OrderItemEntity>()))
            .Callback<object>( o => _resultBeforeMapping = (OrderItemEntity) o)
            .Returns(_orderItemFixture.OrderItemDetailModel);
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new CreateOrderItemCommand(_orderItemFixture.OrderItemCreateModel, _user);
        var handler = new CreateOrderItemCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object,
            _handlerFixture.UserManagerMock.Object);

        var expected = _orderItemFixture.OrderItemDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);

        _resultBeforeMapping.UnitPrice.Should().Be(_orderItemFixture.OrderItemDetailModel.UnitPrice);
        actual.Should().BeEquivalentTo(expected);
    }
}