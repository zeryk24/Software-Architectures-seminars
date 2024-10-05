using System.Security.Claims;
using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.FeedbackCommandHandlers;

public class RemoveFeedbackCommandHandlerTests : IClassFixture<FeedbackFixture>, IClassFixture<HandlerFixture>
{
    private readonly FeedbackFixture _feedbackFixture;
    private readonly HandlerFixture _handlerFixture;
    private readonly ClaimsPrincipal _user;

    public RemoveFeedbackCommandHandlerTests(FeedbackFixture feedbackFixture, HandlerFixture handlerFixture)
    {
        _feedbackFixture = feedbackFixture;
        _handlerFixture = handlerFixture;

        _handlerFixture.FeedbackRepositoryMock.Setup(f => f.GetByIdAsync(_feedbackFixture.MealFeedbackEntity.Id))
            .ReturnsAsync(_feedbackFixture.MealFeedbackEntity);
        _handlerFixture.FeedbackRepositoryMock.Setup(f => f.RemoveAsync(It.IsNotIn(_feedbackFixture.MealFeedbackEntity.Id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.FeedbackRepository)
            .Returns(_handlerFixture.FeedbackRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

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
        var request = new RemoveFeedbackCommand(_feedbackFixture.MealFeedbackEntity.Id, _user);
        var handler = new RemoveFeedbackCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object,
            _handlerFixture.UserManagerMock.Object);

        Assert.True(await handler.Handle(request, new CancellationToken()));
        _handlerFixture.FeedbackRepositoryMock.Verify(f => f.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}