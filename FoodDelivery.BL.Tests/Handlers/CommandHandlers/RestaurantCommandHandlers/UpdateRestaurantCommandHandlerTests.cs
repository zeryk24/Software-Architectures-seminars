using FluentAssertions;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.RestaurantCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class UpdateRestaurantCommandHandlerTests : IClassFixture<RestaurantFixture>, IClassFixture<HandlerFixture>
{
    private readonly RestaurantFixture _restaurantFixture;
    private readonly HandlerFixture _handlerFixture;
    
    public UpdateRestaurantCommandHandlerTests(RestaurantFixture restaurantFixture, HandlerFixture handlerFixture)
    {
        _restaurantFixture = restaurantFixture;
        _handlerFixture = handlerFixture;

        _handlerFixture.RestaurantRepositoryMock.Setup(r => r.GetByIdAsync(restaurantFixture.RestaurantEntity.Id))
            .ReturnsAsync(restaurantFixture.RestaurantEntity);
        _handlerFixture.RestaurantRepositoryMock.Setup(r => r.Update(It.IsNotIn(restaurantFixture.RestaurantEntity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantEntity>(It.IsAny<RestaurantUpdateModel>()))
            .Returns(restaurantFixture.RestaurantEntity);
        _handlerFixture.MapperMock.Setup(m => m.Map<RestaurantDetailModel>(It.IsAny<RestaurantEntity>()))
            .Returns(restaurantFixture.RestaurantDetailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.RestaurantRepository)
            .Returns(_handlerFixture.RestaurantRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new UpdateRestaurantCommand(_restaurantFixture.RestaurantUpdateModel);
        var handler = new UpdateRestaurantCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var expected = _restaurantFixture.RestaurantDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);

        actual.Should().BeEquivalentTo(expected);
        _handlerFixture.RestaurantRepositoryMock.Verify(r => r.Update(It.IsAny<RestaurantEntity>()), Times.Once);
    }
}