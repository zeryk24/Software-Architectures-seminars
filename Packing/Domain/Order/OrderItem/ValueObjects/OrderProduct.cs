using ErrorOr;
using Packing.Domain.Common;

namespace Packing.Domain.Order.OrderItem.ValueObjects;

public class OrderProduct : ValueObject
{
    //TODO: add image
    
    public Guid GoodsId { get; private set; }
    public int Amount { get; private set; }

    private OrderProduct(Guid goodsId, int amount)
    {
        GoodsId = goodsId;
        Amount = amount;
    }

    public static ErrorOr<OrderProduct> Create(Guid goodsId, int amount)
    {
        if (amount <= 0)
            return Error.Validation(OrderErrors.ProductAmountCanNotBeNegative);

        return new OrderProduct(goodsId, amount);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return GoodsId;
        yield return Amount;
    }
}