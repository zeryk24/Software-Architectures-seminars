using Packing.Domain.Common;

namespace Packing.Domain.Order.ValueObjects;

public class OrderId : ValueObject
{
    public Guid Value { get; private set; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId Create(Guid id) => new(id);
    public static OrderId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}