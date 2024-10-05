using System.Security.Claims;
using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.AddressModels;
using Moq;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.AddressCommandHandlers;

public class UpdateCustomerAddressCommandHandlerTests : IClassFixture<AddressFixture>, IClassFixture<HandlerFixture>
{
    private readonly AddressFixture _addressFixture;
    private readonly HandlerFixture _handlerFixture;
    private readonly ClaimsPrincipal _user;

    public UpdateCustomerAddressCommandHandlerTests(AddressFixture addressFixture, HandlerFixture handlerFixture)
    {
        _addressFixture = addressFixture;
        _handlerFixture = handlerFixture;
        
        _handlerFixture.AddressRepositoryMock.Setup(a => a.Update(It.IsNotIn(addressFixture.AddressEntity)))
            .Throws(new ArgumentException());
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressEntity>(It.IsAny<AddressUpdateModel>()))
            .Returns(addressFixture.AddressEntity);
        _handlerFixture.MapperMock.Setup(m => m.Map<AddressDetailModel>(It.IsAny<AddressEntity>()))
            .Returns(addressFixture.AddressDetailModel);
        _handlerFixture.UnitOfWorkMock.SetupGet(u => u.AddressRepository)
            .Returns(_handlerFixture.AddressRepositoryMock.Object);
        _handlerFixture.UnitOfWorkProviderMock.Setup(u => u.Create())
            .Returns(_handlerFixture.UnitOfWorkMock.Object);

        _user = new ClaimsPrincipal();
        _handlerFixture.UserManagerMock.Setup(u => u.GetUserAsync(_user))
            .ReturnsAsync(new UserEntity
            {
                AddressId = _addressFixture.AddressEntity.Id
            });
    }

    [Fact]
    public async Task Handle_ValidRequest_ValidResult()
    {
        var request = new UpdateCustomerAddressCommand(_addressFixture.AddressUpdateModel, _user);

        var handler = new UpdateCustomerAddressCommandHandler(_handlerFixture.UnitOfWorkProviderMock.Object,
            _handlerFixture.MapperMock.Object,
            _handlerFixture.UserManagerMock.Object);

        var expected = _addressFixture.AddressDetailModel;
        var actual = await handler.Handle(request, CancellationToken.None);
        
        Assert.Equal(expected, actual);
        _handlerFixture.AddressRepositoryMock.Verify(a => a.Update(It.IsAny<AddressEntity>()), Times.Once);
    }
}