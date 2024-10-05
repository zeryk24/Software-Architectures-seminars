using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Queries.OrderQueries;

public record GetAllOrdersQuery(int Page = -1, int PageSize = -1) : IRequest<List<OrderListModel>>;
