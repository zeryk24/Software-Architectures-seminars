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

public class UpdateMealCommandHandlerTests : IClassFixture<MealFixture>, IClassFixture<HandlerFixture>
{
    private readonly MealFixture _mealFixture;
    private readonly HandlerFixture _handlerFixture;
    
    public UpdateMealCommandHandlerTests(MealFixture mealFixture, HandlerFixture handlerFixture)
    {
        _mealFixture = mealFixture;
        _handlerFixture = handlerFixture;

        _handlerFixture.MealRepositoryMock.Setup(m => m.GetByIdAsync(mealFixture.MealEntity.Id))
            .ReturnsAsync(mealFixture.MealEntity);
        _handlerFixture.MealRepositoryMock.Setup(f => f.Update(It.IsNotIn(mealFixture.MealEntity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<MealEntity>(It.IsAny<MealUpdateModel>()))
            .Returns(mealFixture.MealEntity);
        _handlerFixture.MapperMock.Setup(m => m.Map<MealDetailModel>(It.IsAny<MealEntity>()))
            .Returns(mealFixture.MealDetailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.MealRepository)
            .Returns(_handlerFixture.MealRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new UpdateMealCommand(_mealFixture.MealUpdateModel);
        var handler = new UpdateMealCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var expected = _mealFixture.MealDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);
        
        Assert.Equal(expected, actual);
        _handlerFixture.MealRepositoryMock.Verify(a => a.Update(It.IsAny<MealEntity>()), Times.Once);
    }
}