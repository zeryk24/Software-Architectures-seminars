using System.Security.Claims;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.MealCommandHandlers;

public class CreateMealCommandHandlerTests : IClassFixture<MealFixture>,IClassFixture<HandlerFixture>
{
    private readonly MealFixture _mealFixture;
    private readonly HandlerFixture _handlerFixture;

    public CreateMealCommandHandlerTests(MealFixture mealFixture, HandlerFixture handlerFixture)
    {
        _mealFixture = mealFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.MealRepositoryMock.Setup(m => m.Insert(It.IsAny<MealEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.MealRepository)
            .Returns(_handlerFixture.MealRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<MealEntity>(mealFixture.MealCreateModel))
            .Returns(mealFixture.MealEntity);
        _handlerFixture.MapperMock.Setup(m => m.Map<MealDetailModel>(mealFixture.MealEntity))
            .Returns(mealFixture.MealDetailModel);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new CreateMealCommand(_mealFixture.MealCreateModel);
        var handler = new CreateMealCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var expected = _mealFixture.MealDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);
     
        Assert.Equal(expected, actual);
    }
}