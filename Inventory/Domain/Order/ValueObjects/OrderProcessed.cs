using Inventory.Domain.Common;

namespace Inventory.Domain.Order.ValueObjects;

public class OrderProcessed : ValueObject
{
    public bool IsProcessed { get; private set; }
    public DateTime? ProcessedAt { get; private set; }

    private OrderProcessed(bool isProcessed, DateTime? processedAt)
    {
        IsProcessed = isProcessed;
        ProcessedAt = processedAt;
    }

    public static OrderProcessed Create(DateTime? processedAt = null)
    {
        if (processedAt is null)
            return new OrderProcessed(false, null);;
        
        return new OrderProcessed(true, processedAt);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return IsProcessed;
        yield return ProcessedAt;
    }
}