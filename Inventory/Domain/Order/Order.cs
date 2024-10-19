using Inventory.Domain.Common;
using Inventory.Domain.Order.ValueObjects;

namespace Inventory.Domain.Order;

public class Order : AggregateRoot<OrderId>
{
    private List<OrderItem.OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem.OrderItem> OrderItems => _orderItems.AsReadOnly();

    public OrderAddress OrderAddress { get; private set; }
    
    private Order() { }
    private Order(OrderId id, OrderAddress orderAddress, IEnumerable<OrderItem.OrderItem> orderItems) : base(id)
    {
        OrderAddress = orderAddress;
        _orderItems = orderItems.ToList();
    }

    public static Order Create(OrderAddress orderAddress, IEnumerable<OrderItem.OrderItem> orderItems) => new(OrderId.CreateUnique(), orderAddress, orderItems);
}

public static class OrderErrors
{
    public const string OrderNotFound = nameof(OrderNotFound);
}