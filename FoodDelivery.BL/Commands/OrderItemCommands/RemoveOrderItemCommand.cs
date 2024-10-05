using MediatR;

namespace FoodDelivery.BL.Commands.OrderItemCommands;

public record RemoveOrderItemCommand(int Id) : IRequest<bool>;

