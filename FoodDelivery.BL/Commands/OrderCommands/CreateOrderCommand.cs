using System.Security.Claims;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Commands.OrderCommands;

public record CreateOrderCommand(int RestaurantId, ClaimsPrincipal User) : IRequest<OrderListModel>;

