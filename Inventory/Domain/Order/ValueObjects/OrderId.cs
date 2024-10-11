using Inventory.Domain.Common;

namespace Inventory.Domain.Order.ValueObjects;

public class OrderId : ValueObject
{
    public Guid Value { get; private set; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId CreateUnique() => new OrderId(Guid.NewGuid());
    public static OrderId Create(Guid value) => new OrderId(value);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}