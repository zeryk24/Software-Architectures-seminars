using Inventory.Domain.Common;
using Inventory.Domain.Order.OrderItem.ValueObjects;

namespace Inventory.Domain.Order.OrderItem;

public class OrderItem : Entity<OrderItemId>
{
    public OrderItemGoods Goods { get; private set; }
    public OrderItemAmount Amount { get; private set; }
    public OrderItemPrice Price { get; private set; }
    
    private OrderItem() { }
    private OrderItem(OrderItemGoods goods, OrderItemAmount amount, OrderItemPrice price)
    {
        Goods = goods;
        Amount = amount;
        Price = price;
    }

    public static OrderItem Create(OrderItemGoods goodsId, OrderItemAmount amount, OrderItemPrice price) =>
        new OrderItem(goodsId, amount, price);
}

public static class OrderItemErrors
{
    public const string OrderItemPriceCanNotBeNegative = nameof(OrderItemPriceCanNotBeNegative);
    public const string OrderItemAmountCanNotBeNegative = nameof(OrderItemAmountCanNotBeNegative);
}