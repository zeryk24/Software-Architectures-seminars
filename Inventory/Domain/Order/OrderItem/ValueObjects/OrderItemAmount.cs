using Inventory.Domain.Common;
using ErrorOr;

namespace Inventory.Domain.Order.OrderItem.ValueObjects;

public class OrderItemAmount : ValueObject
{
    public int Value { get; private set; }

    private OrderItemAmount(int value)
    {
        Value = value;
    }

    public static ErrorOr<OrderItemAmount> Create(int value)
    {
        if (value < 0)
            return Error.Validation(OrderItemErrors.OrderItemAmountCanNotBeNegative);
        
        return new OrderItemAmount(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}