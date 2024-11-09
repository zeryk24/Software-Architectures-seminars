using ErrorOr;
using Packing.Domain.Common;
using Packing.Domain.Order.DomainEvents;
using Packing.Domain.Order.ValueObjects;

namespace Packing.Domain.Order;

public class Order : AggregateRoot<OrderId>
{
    //Could add things like "present package", "eco package" etc.
    
    private List<OrderItem.OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem.OrderItem> OrderItems => _orderItems.AsReadOnly();
    
    public OrderPackaged Packaged { get; private set; }
    
    private Order(){}
    private Order(OrderId id, IEnumerable<OrderItem.OrderItem> orderItems) : base(id)
    {
        Packaged = OrderPackaged.Create(null);
        _orderItems = orderItems.ToList();
    }

    public static Order Create(OrderId id, IEnumerable<OrderItem.OrderItem> orderItems) => new(id, orderItems);
    
    public ErrorOr<Success> JustPackaged(DateTime now)
    {
        if (Packaged.IsPackaged)
            return Error.Validation(OrderErrors.OrderWasAlreadyPackaged);
        
        Packaged = OrderPackaged.Create(now);
        
        Raise(new OrderPackagedEvent(Guid.NewGuid(), this));

        return Result.Success;
    }
}

public static class OrderErrors
{
    public const string OrderNotFound = nameof(OrderNotFound);
    public const string OrderWasAlreadyPackaged = nameof(OrderWasAlreadyPackaged);
    public const string ProductAmountCanNotBeNegative = nameof(ProductAmountCanNotBeNegative);
}