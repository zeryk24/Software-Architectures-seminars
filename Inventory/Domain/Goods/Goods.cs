using Inventory.Domain.Common;
using Inventory.Domain.Goods.ValueObjects;
using ErrorOr;

namespace Inventory.Domain.Goods;

public class Goods : AggregateRoot<GoodsId>
{
    public GoodsName Name { get; private set; }
    public GoodsAmount Amount { get; private set; }
    
    private Goods() { }
    private Goods(GoodsId id, GoodsName name, GoodsAmount amount) : base(id)
    {
        Name = name;
        Amount = amount;
    }

    public bool IsAmountAvailable(int amount) => Amount.UnitsAmount >= amount;
    
    public static ErrorOr<Goods> Create(string name, int amount)
    {
        var goodsName = GoodsName.Create(name);
        var goodsAmount = GoodsAmount.Create(amount);

        if (goodsAmount.IsError)
            return goodsAmount.Errors;
        
        return new Goods(GoodsId.CreateUnique(), goodsName, goodsAmount.Value);
    }
}

public static class GoodsErrors
{
    public const string GoodsAmountCanNotBeNegative = nameof(GoodsAmountCanNotBeNegative);
    public const string GoodsNotFound = nameof(GoodsNotFound);
}