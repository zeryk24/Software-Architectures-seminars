using ErrorOr;
using Inventory.Domain.Common;

namespace Inventory.Domain.Goods.ValueObjects;

public class GoodsAmount : ValueObject
{
    public int UnitsAmount { get; private set; }

    private GoodsAmount(int unitsAmount)
    {
        UnitsAmount = unitsAmount;
    }

    public static ErrorOr<GoodsAmount> Create(int unitsAmount)
    {
        if (unitsAmount < 0)
            return Error.Validation(GoodsErrors.GoodsAmountCanNotBeNegative);

        return new GoodsAmount(unitsAmount);
    }
    
    public ErrorOr<Success> Restock(int amount)
    {
        if (amount < 0)
            return Error.Validation(GoodsErrors.GoodsAmountCanNotBeNegative);

        UnitsAmount += amount;

        return Result.Success;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return UnitsAmount;
    }
}