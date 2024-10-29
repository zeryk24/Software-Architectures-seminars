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

    public ErrorOr<Success> Modify(string name, int amount)
    {
        Name = GoodsName.Create(name);
        var amountResult = GoodsAmount.Create(amount);

        if (amountResult.IsError)
            return amountResult.Errors;

        Amount = amountResult.Value;

        return Result.Success;
    }

    public bool IsAmountAvailable(int amount) => Amount.Value >= amount;
    
    public ErrorOr<Success> Restock(int amount)
    {
        if (amount < 0)
            return Error.Validation(GoodsErrors.RestockedAmountCanNotBeNegative);
        
        var result = GoodsAmount.Create(Amount.Value + amount);

        if (result.IsError)
            return result.Errors;

        Amount = result.Value;

        return Result.Success;
    }

    public ErrorOr<Success> DecreaseAmount(int amount)
    {
        var newAmount = Amount.Value - amount;
        if (newAmount < 0)
            return Error.Validation(GoodsErrors.NotEnoughtGoods);

        var result = GoodsAmount.Create(newAmount);
        if (result.IsError)
            return result.Errors;
        
        Amount = result.Value;

        return Result.Success;
    }

    public static ErrorOr<Goods> Create(string name, int amount)
    {
        return Create(GoodsId.CreateUnique(), name, amount);
    }
    
    public static ErrorOr<Goods> Create(GoodsId Id, string name, int amount)
    {
        var goodsName = GoodsName.Create(name);
        var goodsAmount = GoodsAmount.Create(amount);

        if (goodsAmount.IsError)
            return goodsAmount.Errors;
        
        return new Goods(Id, goodsName, goodsAmount.Value);
    }
    
}

public static class GoodsErrors
{
    public const string NotEnoughtGoods = nameof(NotEnoughtGoods);
    public const string GoodsAmountCanNotBeNegative = nameof(GoodsAmountCanNotBeNegative);
    public const string RestockedAmountCanNotBeNegative = nameof(RestockedAmountCanNotBeNegative);
    public const string GoodsNotFound = nameof(GoodsNotFound);
}