using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.MealCommandHandlers;

public class RemoveMealCommandHandlerTests : IClassFixture<MealFixture>, IClassFixture<HandlerFixture>
{
    private readonly MealFixture _mealFixture;
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveMealCommandHandlerTests(MealFixture mealFixture, HandlerFixture handlerFixture)
    {
        _mealFixture = mealFixture;
        _handlerFixture = handlerFixture;

        _handlerFixture.MealRepositoryMock.Setup(m => m.RemoveAsync(It.IsIn(mealFixture.MealEntity.Id)))
            .ReturnsAsync(true);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.MealRepository)
            .Returns(_handlerFixture.MealRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new RemoveMealCommand(_mealFixture.MealEntity.Id);
        var handler = new RemoveMealCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result);
        _handlerFixture.MealRepositoryMock.Verify(m => m.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}