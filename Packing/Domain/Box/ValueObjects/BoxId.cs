using Packing.Domain.Common;

namespace Packing.Domain.Box.ValueObjects;

public class BoxId : ValueObject
{
    public Guid Value { get; private set; }
    
    private BoxId(Guid value)
    {
        Value = value;
    }

    public static BoxId Create(Guid id) => new(id);
    public static BoxId CreateUnique() => new(Guid.NewGuid());
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}