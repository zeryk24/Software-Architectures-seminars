using Inventory.Contracts.IntegrationEvents;
using Inventory.Contracts.IntegrationEvents.InventoryOrderProcessed;
using Inventory.Domain.Common;

namespace Inventory.Domain.Order.DomainEvents;

public static class OrderDomainEventsMapper
{
    public static IntegrationEvent? MapToIntegrationEvent(IOrderDomainEvent @event)
    {
        return @event switch
        {
            OrderProcessedDomainEvent e => MapOrderProcessed(e),
            _ => null
        };
    }

    private static InventoryOrderProcessedIntegrationEvent? MapOrderProcessed(OrderProcessedDomainEvent domainEvent)
    {
        return new(
            domainEvent.Id,
            new(domainEvent.Order.Id.Value,
                domainEvent.Order.OrderItems.Select(oi => new InventoryOrderProcessedIntegrationEvent.OrderItem(
                    oi.Id.Value,
                    oi.Goods.Id,
                    oi.Amount.Value)
                )
            )
        );
    }
}