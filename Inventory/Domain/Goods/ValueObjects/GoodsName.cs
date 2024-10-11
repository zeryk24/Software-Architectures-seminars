using Inventory.Domain.Common;

namespace Inventory.Domain.Goods.ValueObjects;

public class GoodsName : ValueObject
{
    public string Value { get; private set; }

    private GoodsName() {}
    
    private GoodsName(string value)
    {
        Value = value;
    }

    public static GoodsName Create(string name) => new GoodsName(name);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}