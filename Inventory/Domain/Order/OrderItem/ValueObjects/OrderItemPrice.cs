using Inventory.Domain.Common;
using Inventory.Domain.Common.Enums;
using ErrorOr;

namespace Inventory.Domain.Order.OrderItem.ValueObjects;

public class OrderItemPrice : ValueObject
{
    public double Value { get; private set; }
    public Currency Currency { get; private set; }
    
    private OrderItemPrice(double value, Currency currency)
    {
        Value = value;
        Currency = currency;
    }

    public static ErrorOr<OrderItemPrice> Create(double value, Currency currency)
    {
        if (value < 0)
            return Error.Validation(OrderItemErrors.OrderItemPriceCanNotBeNegative);
                
        return new OrderItemPrice(value, currency);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
}

