using MediatR;

namespace FoodDelivery.BL.Commands.AddressCommands;

public record RemoveAddressCommand(int Id) : IRequest<bool>;

