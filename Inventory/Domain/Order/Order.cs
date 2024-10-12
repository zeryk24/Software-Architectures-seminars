using Inventory.Domain.Common;
using Inventory.Domain.Order.ValueObjects;

namespace Inventory.Domain.Order;

public class Order : AggregateRoot<OrderId>
{
    private List<OrderItem.OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem.OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Address Address { get; private set; }
    
    private Order() { }
    private Order(Address address)
    {
        Address = address;
    }

    public static Order Create(Address address) => new(address);
}