using Packing.Domain.Common;

namespace Packing.Domain.Order.ValueObjects;

public class OrderPackaged : ValueObject
{
    public bool IsPackaged { get; private set; }
    public DateTime? PackagedAt { get; private set; }
    
    private OrderPackaged(bool isPackaged, DateTime? packagedAt)
    {
        IsPackaged = isPackaged;
        PackagedAt = packagedAt;
    }
    
    public static OrderPackaged Create(DateTime? processedAt = null)
    {
        if (processedAt is null)
            return new OrderPackaged(false, null);;
        
        return new OrderPackaged(true, processedAt);
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return IsPackaged;
        yield return PackagedAt;
    }
}