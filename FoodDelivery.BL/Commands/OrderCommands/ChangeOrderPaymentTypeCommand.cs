using System.Security.Claims;
using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Commands.OrderCommands;

public record ChangeOrderPaymentTypeCommand(int OrderId, PaymentType PaymentType, ClaimsPrincipal User) : IRequest<OrderDetailModel>;