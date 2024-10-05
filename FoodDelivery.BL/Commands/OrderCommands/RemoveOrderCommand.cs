using System.Security.Claims;
using MediatR;

namespace FoodDelivery.BL.Commands.OrderCommands;

public record RemoveOrderCommand(int Id) : IRequest<bool>;

