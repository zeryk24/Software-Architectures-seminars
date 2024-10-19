using ErrorOr;
using Inventory.Domain.Common;

namespace Inventory.Domain.Goods.ValueObjects;

public class GoodsAmount : ValueObject
{
    public int Value { get; private set; }

    private GoodsAmount(int value)
    {
        Value = value;
    }

    public static ErrorOr<GoodsAmount> Create(int value)
    {
        if (value < 0)
            return Error.Validation(GoodsErrors.GoodsAmountCanNotBeNegative);

        return new GoodsAmount(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}