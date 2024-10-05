using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.AddressCommandHandlers;

public class RemoveAddressCommandHandlerTests : IClassFixture<AddressFixture>, IClassFixture<HandlerFixture>
{
    private readonly AddressFixture _addressFixture;
    private readonly HandlerFixture _handlerFixture;
    
    public RemoveAddressCommandHandlerTests(AddressFixture addressFixture, HandlerFixture handlerFixture)
    {
        _addressFixture = addressFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.AddressRepositoryMock.Setup(a => a.RemoveAsync(It.IsNotIn(addressFixture.AddressEntity.Id)))
            .Throws(new ArgumentException());
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.AddressRepository)
            .Returns(_handlerFixture.AddressRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new RemoveAddressCommand(_addressFixture.AddressEntity.Id);
        var handler = new RemoveAddressCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);

        var result = await handler.Handle(request, CancellationToken.None);
        
        Assert.True(result);
        
        _handlerFixture.AddressRepositoryMock.Verify(a => a.RemoveAsync(It.IsAny<int>()), Times.Once);
    }
}