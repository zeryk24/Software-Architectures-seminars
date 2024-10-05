using System.Security.Claims;
using FluentAssertions;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.RestaurantModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class CreateRestaurantCommandHandlerTests : IClassFixture<RestaurantFixture>, IClassFixture<HandlerFixture>
{
    private readonly RestaurantFixture _restaurantFixture;
    private readonly HandlerFixture _handlerFixture;
    private readonly ClaimsPrincipal _user;

    public CreateRestaurantCommandHandlerTests(RestaurantFixture restaurantFixture,HandlerFixture handlerFixture)
    {
        _restaurantFixture = restaurantFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.RestaurantRepositoryMock.Setup(m => m.Insert(It.IsAny<RestaurantEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.RestaurantRepository)
            .Returns(_handlerFixture.RestaurantRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantEntity>(restaurantFixture.RestaurantCreateModel))
            .Returns(restaurantFixture.RestaurantEntity);
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantDetailModel>(restaurantFixture.RestaurantEntity))
            .Returns(restaurantFixture.RestaurantDetailModel);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new CreateRestaurantCommand(_restaurantFixture.RestaurantCreateModel);
        var handler = new CreateRestaurantCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var expected = _restaurantFixture.RestaurantDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Should().BeEquivalentTo(expected);
    }
}