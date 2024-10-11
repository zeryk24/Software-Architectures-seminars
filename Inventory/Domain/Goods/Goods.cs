using Inventory.Domain.Common;
using Inventory.Domain.Goods.ValueObjects;

namespace Inventory.Domain.Goods;

public class Goods : AggregateRoot<GoodsId>
{
    public GoodsName Name { get; private set; }
    public GoodsAmount Amount { get; private set; }
    
    private Goods(GoodsName name, GoodsAmount amount)
    {
        Name = name;
        Amount = amount;
    }

    public static Goods Create(GoodsName name, GoodsAmount amount) => new Goods(name, amount);
}

public static class GoodsErrors
{
    public const string GoodsAmountCanNotBeNegative = nameof(GoodsAmountCanNotBeNegative);
}