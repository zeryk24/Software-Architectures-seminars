using Packing.Domain.Common;
using Packing.Domain.Order.OrderItem.ValueObjects;
using ErrorOr;

namespace Packing.Domain.Order.OrderItem;

public class OrderItem : Entity<OrderItemId>
{
    public OrderProduct Product { get; private set; }

    private OrderItem(){}

    private OrderItem(OrderItemId id, OrderProduct product) : base(id)
    {
        Product = product;
    }

    public static ErrorOr<OrderItem> Create(Guid id, Guid goodsId, int amount)
    {
        var product = OrderProduct.Create(goodsId, amount);

        if (product.IsError)
            return product.Errors;
        
        return new OrderItem(OrderItemId.Create(id), product.Value);
    }
}