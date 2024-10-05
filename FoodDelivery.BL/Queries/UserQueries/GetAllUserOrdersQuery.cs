using System.Security.Claims;
using FoodDelivery.Shared.Models.OrderModels;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;

namespace FoodDelivery.BL.Queries.UserQueries;

public record GetAllUserOrdersQuery(ClaimsPrincipal User) : IRequest<List<OrderListModel>>;


