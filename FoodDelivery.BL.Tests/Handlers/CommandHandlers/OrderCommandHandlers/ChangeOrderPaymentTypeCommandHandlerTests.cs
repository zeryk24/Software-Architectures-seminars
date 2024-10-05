using System.Security.Claims;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.OrderModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderCommandHandlers;

public class ChangeOrderPaymentTypeCommandHandlerTests : IClassFixture<OrderFixture>, IClassFixture<HandlerFixture>
{
    private readonly OrderFixture _orderFixture;
    private readonly HandlerFixture _handlerFixture;
    private readonly ClaimsPrincipal _user;

    public ChangeOrderPaymentTypeCommandHandlerTests(OrderFixture orderFixture, HandlerFixture handlerFixture)
    {
        _orderFixture = orderFixture;
        _handlerFixture = handlerFixture;

        _handlerFixture.OrderRepositoryMock.Setup(o => o.GetByIdAsync(orderFixture.OrderEntity.Id))
            .ReturnsAsync(orderFixture.OrderEntity);
        _handlerFixture.OrderRepositoryMock.Setup(o => o.Insert(It.IsAny<OrderEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderRepository)
            .Returns(_handlerFixture.OrderRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<OrderDetailModel>(orderFixture.OrderEntity))
            .Returns(orderFixture.OrderDetailModel);

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
        var request = new ChangeOrderPaymentTypeCommand(_orderFixture.OrderEntity.Id, PaymentType.Card, _user);
        var handler = new ChangeOrderPaymentTypeCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object,
            _handlerFixture.UserManagerMock.Object);

        var expected = _orderFixture.OrderDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);
        
        Assert.Equal(expected, actual);
    }
}