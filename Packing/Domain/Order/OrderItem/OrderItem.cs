using Packing.Domain.Common;
using Packing.Domain.Order.OrderItem.ValueObjects;

namespace Packing.Domain.Order.OrderItem;

public class OrderItem : Entity<OrderItemId>
{
    public OrderProduct Product { get; private set; }

    private OrderItem(){}

    private OrderItem(OrderItemId id, OrderProduct product) : base(id)
    {
        Product = product;
    }

    public static OrderItem Create(OrderItemId id, OrderProduct product) => new(id, product);
}