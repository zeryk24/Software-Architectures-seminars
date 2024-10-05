using System.Security.Claims;
using MediatR;

namespace FoodDelivery.BL.Commands.RestaurantCommands;

public record RemoveRestaurantCommand(int Id) : IRequest<bool>;

