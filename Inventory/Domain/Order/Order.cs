using Inventory.Domain.Common;
using Inventory.Domain.Order.ValueObjects;

namespace Inventory.Domain.Order;

public class Order : AggregateRoot<OrderId>
{
    private List<OrderItem.OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem.OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Address Address { get; private set; }
    
    private Order() { }
    private Order(OrderId id, Address address, IEnumerable<OrderItem.OrderItem> orderItems) : base(id)
    {
        Address = address;
        _orderItems = orderItems.ToList();
    }

    public static Order Create(Address address, IEnumerable<OrderItem.OrderItem> orderItems) => new(OrderId.CreateUnique(), address, orderItems);
}

public static class OrderErrors
{
    public const string OrderNotFound = nameof(OrderNotFound);
}