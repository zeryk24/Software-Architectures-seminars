using System.Security.Claims;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Commands.OrderCommands;

public record OrderCompletedCommand(int OrderId, ClaimsPrincipal User) : IRequest<OrderDetailModel>;