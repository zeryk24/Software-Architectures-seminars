using Inventory.Contracts.IntegrationEvents.InventoryOrderProcessed;
using Marten;

namespace EventLog.Order;

public class InventoryOrderProcessedHandler(IDocumentSession session)
{
    public async Task Handle(InventoryOrderProcessedIntegrationEvent message)
    {
        session.Events.Append(message.Order.Id, message);

        await session.SaveChangesAsync();
    }
}