using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderCommandHandlers;

public class RemoveOrderCommandHandlerTests : IClassFixture<OrderFixture>, IClassFixture<HandlerFixture>
{
    private readonly OrderFixture _orderFixture;
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveOrderCommandHandlerTests(OrderFixture orderFixture, HandlerFixture handlerFixture)
    {
        _orderFixture = orderFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.OrderRepositoryMock.Setup(o => o.RemoveAsync(It.IsIn(orderFixture.OrderEntity.Id)))
            .ReturnsAsync(true);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderRepository)
            .Returns(_handlerFixture.OrderRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new RemoveOrderCommand(_orderFixture.OrderEntity.Id);
        var handler = new RemoveOrderCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.True(result);
        _handlerFixture.OrderRepositoryMock.Verify(o => o.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}