using System.Security.Claims;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Queries.OrderQueries;

public record GetOrderQuery(int OrderId, ClaimsPrincipal User) : IRequest<OrderDetailModel>;