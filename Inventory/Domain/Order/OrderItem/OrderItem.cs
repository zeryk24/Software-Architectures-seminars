using Inventory.Domain.Common;
using Inventory.Domain.Goods.ValueObjects;
using Inventory.Domain.Order.OrderItem.ValueObjects;

namespace Inventory.Domain.Order.OrderItem;

public class OrderItem : Entity<OrderItemId>
{
    public GoodsId GoodsId { get; private set; }
    public OrderItemAmount Amount { get; private set; }
    public OrderItemPrice Price { get; private set; }

    private OrderItem(GoodsId goodsId, OrderItemAmount amount, OrderItemPrice price)
    {
        GoodsId = goodsId;
        Amount = amount;
        Price = price;
    }

    public static OrderItem Create(GoodsId goodsId, OrderItemAmount amount, OrderItemPrice price) =>
        new OrderItem(goodsId, amount, price);
}

public static class OrderItemErrors
{
    public const string OrderItemPriceCanNotBeNegative = nameof(OrderItemPriceCanNotBeNegative);
    public const string OrderItemAmountCanNotBeNegative = nameof(OrderItemAmountCanNotBeNegative);
}