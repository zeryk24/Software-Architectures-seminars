using FluentAssertions;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class RemoveOrderItemCommandHandlerTests : IClassFixture<OrderItemFixture>, IClassFixture<HandlerFixture>
{
    private readonly OrderItemFixture _orderItemFixture;
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveOrderItemCommandHandlerTests(OrderItemFixture orderItemFixture, HandlerFixture handlerFixture)
    {
        _orderItemFixture = orderItemFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.OrderItemRepositoryMock.Setup(o => o.RemoveAsync(It.IsIn(orderItemFixture.OrderItemEntity.Id)))
            .ReturnsAsync(true);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.OrderItemRepository)
            .Returns(_handlerFixture.OrderItemRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new RemoveOrderItemCommand(_orderItemFixture.OrderItemEntity.Id);
        var handler = new RemoveOrderItemCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var result = await handler.Handle(request, CancellationToken.None);
        _handlerFixture.OrderItemRepositoryMock.Verify(o => o.RemoveAsync(It.IsAny<int>()), Times.Once);
        result.Should().BeTrue();
    }
}