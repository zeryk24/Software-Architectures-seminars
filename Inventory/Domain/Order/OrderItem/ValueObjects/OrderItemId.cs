using Inventory.Domain.Common;

namespace Inventory.Domain.Order.OrderItem.ValueObjects;

public class OrderItemId : ValueObject
{
    public Guid Value { get; private set; }
    
    private OrderItemId(Guid value)
    {
        Value = value;
    }

    public static OrderItemId CreateUnique() => new OrderItemId(Guid.NewGuid());
    public static OrderItemId Create(Guid value) => new OrderItemId(value);
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}