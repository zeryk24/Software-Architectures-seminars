using Inventory.Domain.Common;

namespace Inventory.Domain.Order.OrderItem.ValueObjects;

public class OrderItemGoods : ValueObject
{
    public Guid Id { get; private set; }

    private OrderItemGoods(Guid id)
    {
        Id = id;
    }

    public static OrderItemGoods Create(Guid id) => new OrderItemGoods(id);
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }
}