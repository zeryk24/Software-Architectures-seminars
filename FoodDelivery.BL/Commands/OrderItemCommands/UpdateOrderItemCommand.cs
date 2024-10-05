using System.Security.Claims;
using FoodDelivery.Shared.Models.OrderItemsModels;
using MediatR;

namespace FoodDelivery.BL.Commands.OrderItemCommands;

public record UpdateOrderItemCommand(OrderItemUpdateModel OrderItemUpdateModel, ClaimsPrincipal User) : IRequest<OrderItemDetailModel>;

