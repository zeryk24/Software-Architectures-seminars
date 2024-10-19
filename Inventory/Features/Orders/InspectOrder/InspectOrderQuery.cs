using ErrorOr;
using Inventory.Domain.Common.Enums;
using Inventory.Domain.Goods;
using Inventory.Domain.Goods.ValueObjects;
using Inventory.Domain.Order;
using Inventory.Domain.Order.ValueObjects;
using Inventory.Features.Services;

namespace Inventory.Features.Orders.InspectOrder;

public record InspectOrderQuery(Guid OrderId)
{
    public record Result(Order OrderInspection);
    
    public record Order(Guid Id, IEnumerable<OrderItem> OrderItems, Address Address);

    public record OrderItem(Guid Id, int Amount, OrderItemPrice Price, bool IsAmountAvailable);

    public record OrderItemPrice(Currency Currency, int Value);

    public record Address(State State, string City, string Code, string StreetAndNumber);
}

public class InspectOrderQueryHandler(
    IQueryObject<Order> _orderQueryObject,
    IQueryObject<Domain.Goods.Goods> _goodsQueryObject
    )
{
    public async Task<ErrorOr<InspectOrderQuery.Result>> Handle(InspectOrderQuery query)
    {
        var order = (await _orderQueryObject.Filter(o => o.Id == OrderId.Create(query.OrderId)).ExecuteAsync())
            .SingleOrDefault();

        if (order is null)
            return Error.Validation(OrderErrors.OrderNotFound);

        var goodsIds = order.OrderItems.Select(orderItem => GoodsId.Create(orderItem.Goods.Id)).ToList();

        var goodsDictionary = (await _goodsQueryObject
                .Filter(g => goodsIds.Contains(g.Id))
                .ExecuteAsync())
            .ToDictionary(g => g.Id, g => g);
        
        var orderInspectionItems = GetOrderInspectionItems(order, goodsDictionary);

        return orderInspectionItems.IsError? orderInspectionItems.Errors : new InspectOrderQuery.Result(
            new(
                order.Id.Value,
                orderInspectionItems.Value,
                new(
                    order.OrderAddress.State,
                    order.OrderAddress.City,
                    order.OrderAddress.Code,
                    order.OrderAddress.StreetAndNumber
                )
            )
        );
    }

    private ErrorOr<List<InspectOrderQuery.OrderItem>> GetOrderInspectionItems(Order order, Dictionary<GoodsId, Domain.Goods.Goods> goodsDictionary)
    {
        List<Error> errors = new();
        List<InspectOrderQuery.OrderItem> orderInspectionItems = new();
        
        foreach (var orderItem in order.OrderItems)
        {
            var goodsId = GoodsId.Create(orderItem.Goods.Id);

            if (!goodsDictionary.TryGetValue(goodsId, out var goods))
            {
                errors.Add(Error.Validation(GoodsErrors.GoodsNotFound, orderItem.Id.Value.ToString()));
                continue;
            }

            orderInspectionItems.Add(
                new(
                    orderItem.Id.Value,
                    orderItem.Amount.Value,
                    new(orderItem.Price.Currency, orderItem.Price.Value),
                    goods.IsAmountAvailable(orderItem.Amount.Value)
                )
            );
        }
        
        return errors.Any() ? errors : orderInspectionItems;
    }
}