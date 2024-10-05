using FluentAssertions;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class RemoveRestaurantCommandHandlerTests : IClassFixture<RestaurantFixture>, IClassFixture<HandlerFixture>
{
    private readonly RestaurantFixture _restaurantFixture;
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveRestaurantCommandHandlerTests(RestaurantFixture restaurantFixture, HandlerFixture handlerFixture)
    {
        _restaurantFixture = restaurantFixture;
        _handlerFixture = handlerFixture;

        _handlerFixture.RestaurantRepositoryMock.Setup(r => r.GetByIdAsync(restaurantFixture.RestaurantEntity.Id))
            .ReturnsAsync(restaurantFixture.RestaurantEntity);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.RestaurantRepository)
            .Returns(_handlerFixture.RestaurantRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    { 
        var request = new RemoveRestaurantCommand(_restaurantFixture.RestaurantEntity.Id);
        var handler = new RemoveRestaurantCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var result = await handler.Handle(request, CancellationToken.None);
        result.Should().BeTrue();
    }
}