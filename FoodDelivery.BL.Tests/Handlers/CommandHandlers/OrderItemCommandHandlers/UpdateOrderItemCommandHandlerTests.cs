using System.Security.Claims;
using FluentAssertions;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.OrderModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class UpdateOrderItemCommandHandlerTests : IClassFixture<OrderItemFixture>, IClassFixture<HandlerFixture>
{
    private readonly OrderItemFixture _orderItemFixture;
    private readonly HandlerFixture _handlerFixture;
    private ClaimsPrincipal _user;

    public UpdateOrderItemCommandHandlerTests(OrderItemFixture orderItemFixture,HandlerFixture handlerFixture)
    {
        _orderItemFixture = orderItemFixture;
        _handlerFixture = handlerFixture;

        _user = new ClaimsPrincipal();
        _handlerFixture.UserManagerMock.Setup(u => u.GetUserAsync(_user))
            .ReturnsAsync(new UserEntity
            {
                Id = 1,
            });

        _handlerFixture.OrderRepositoryMock.Setup(o => o.GetByIdAsync(orderItemFixture.OrderItemEntity.OrderId))
            .ReturnsAsync(new OrderEntity
            {
                Id = 1,
                UserId = 1,
            });
        _handlerFixture.OrderItemRepositoryMock.Setup(o => o.GetByIdAsync(orderItemFixture.OrderItemEntity.Id))
            .ReturnsAsync(orderItemFixture.OrderItemEntity);
        _handlerFixture.OrderItemRepositoryMock.Setup(o => o.Update(It.IsNotIn(orderItemFixture.OrderItemEntity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderItemDetailModel>(It.IsAny<OrderItemEntity>()))
            .Returns(orderItemFixture.OrderItemDetailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderItemRepository)
            .Returns(_handlerFixture.OrderItemRepositoryMock.Object);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderRepository)
            .Returns(_handlerFixture.OrderRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new UpdateOrderItemCommand(_orderItemFixture.OrderItemUpdateModel, _user);
        var handler = new UpdateOrderItemCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object,
            _handlerFixture.UserManagerMock.Object);

        var expected = _orderItemFixture.OrderItemDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Should().BeEquivalentTo(expected);
        _handlerFixture.OrderItemRepositoryMock.Verify(o => o.Update(It.IsAny<OrderItemEntity>()), Times.Once);
    }
}