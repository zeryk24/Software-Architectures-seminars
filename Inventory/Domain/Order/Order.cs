using ErrorOr;
using Inventory.Domain.Common;
using Inventory.Domain.Order.DomainEvents;
using Inventory.Domain.Order.ValueObjects;

namespace Inventory.Domain.Order;

public class Order : AggregateRoot<OrderId>
{
    private List<OrderItem.OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem.OrderItem> OrderItems => _orderItems.AsReadOnly();

    public OrderAddress Address { get; private set; }

    public OrderProcessed Processed { get; private set; }
    
    private Order() { }
    private Order(OrderId id, OrderAddress address, IEnumerable<OrderItem.OrderItem> orderItems, OrderProcessed processed) : base(id)
    {
        Address = address;
        _orderItems = orderItems.ToList();
        Processed = processed;
    }

    public static Order Create(OrderAddress orderAddress, IEnumerable<OrderItem.OrderItem> orderItems)
    {
        return Create(OrderId.CreateUnique(), orderAddress, orderItems);
    }

    public static Order Create(OrderId Id, OrderAddress orderAddress, IEnumerable<OrderItem.OrderItem> orderItems)
    {
        return new Order(
            Id,
            orderAddress,
            orderItems,
            OrderProcessed.Create(null)
        );
    }

    public ErrorOr<Success> JustProcessed(DateTime now)
    {
        if (Processed.IsProcessed)
            return Error.Validation(OrderErrors.OrderWasAlreadyProcessed);
        
        Processed = OrderProcessed.Create(now);
        
        Raise(new OrderProcessedEvent(Guid.NewGuid(), this));

        return Result.Success;
    }
}

public static class OrderErrors
{
    public const string OrderNotFound = nameof(OrderNotFound);
    public const string OrderWasAlreadyProcessed = nameof(OrderWasAlreadyProcessed);
}