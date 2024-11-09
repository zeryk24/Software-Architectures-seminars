namespace Inventory.Contracts.IntegrationEvents.InventoryOrderProcessed;

public record InventoryOrderProcessedIntegrationEvent(Guid Id, InventoryOrderProcessedIntegrationEvent.ProcessedOrder Order) : IntegrationEvent(Id)
{
    public record ProcessedOrder(Guid Id, IEnumerable<OrderItem> OrderItems);

    public record OrderItem(Guid Id, Guid GoodsId, int Amount);
}