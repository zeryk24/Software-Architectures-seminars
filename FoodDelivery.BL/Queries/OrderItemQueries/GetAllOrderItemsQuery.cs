using FoodDelivery.Shared.Models.OrderItemsModels;
using MediatR;

namespace FoodDelivery.BL.Queries.OrderItemQueries;

public record GetAllOrderItemsQuery(int Page = -1, int PageSize = -1) : IRequest<List<OrderItemListModel>>;

