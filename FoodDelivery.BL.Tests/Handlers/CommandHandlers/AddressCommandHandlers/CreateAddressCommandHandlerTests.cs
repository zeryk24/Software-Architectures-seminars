using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.AddressModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.AddressCommandHandlers;

public class CreateAddressCommandHandlerTests : IClassFixture<AddressFixture>, IClassFixture<HandlerFixture>
{
    private readonly AddressFixture _addressFixture;
    private readonly HandlerFixture _handlerFixture;

    public CreateAddressCommandHandlerTests(AddressFixture addressFixture, HandlerFixture handlerFixture)
    {
        _addressFixture = addressFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.AddressRepositoryMock.Setup(a => a.Insert(It.IsAny<AddressEntity>()));
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.AddressRepository)
            .Returns(_handlerFixture.AddressRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressEntity>(addressFixture.AddressCreateModel))
            .Returns(addressFixture.AddressEntity);
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressDetailModel>(addressFixture.AddressEntity))
            .Returns(addressFixture.AddressDetailModel);
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new CreateAddressCommand(_addressFixture.AddressCreateModel);
        var handler = new CreateAddressCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object);
        
        var expected = _addressFixture.AddressDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);
        
        Assert.Equal(expected, actual);
    }

}