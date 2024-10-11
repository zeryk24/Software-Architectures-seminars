using Inventory.Domain.Common;

namespace Inventory.Domain.Goods.ValueObjects;

public class GoodsId : ValueObject
{
    public Guid Value { get; private set; }
    
    private GoodsId(Guid value)
    {
        Value = value;
    }

    public static GoodsId CreateUnique() => new GoodsId(Guid.NewGuid());
    public static GoodsId Create(Guid value) => new GoodsId(value);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}