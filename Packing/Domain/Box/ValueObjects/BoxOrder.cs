using Packing.Domain.Common;

namespace Packing.Domain.Box.ValueObjects;

public class BoxOrder : ValueObject
{
    public Guid OrderId { get; private set; }

    private BoxOrder(Guid orderId)
    {
        OrderId = orderId;
    }

    public static BoxOrder Create(Guid orderId) => new(orderId);
    public static BoxOrder Empty() => new(Guid.Empty);
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return OrderId;
    }
}