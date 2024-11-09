using Inventory.Contracts.IntegrationEvents.InventoryOrderProcessed;
using Packing.Application.Order;
using Wolverine;

namespace Packing.Infrastructure.Consumers.Order;

public class InventoryOrderProcessedEventHandler(IMessageBus _sender)
{
    public async Task Handle(InventoryOrderProcessedIntegrationEvent @event)
    {
        var command = new CreateOrderCommand(new CreateOrderCommand.Order(
                @event.Order.Id,
                @event.Order.OrderItems.Select(oi => new CreateOrderCommand.OrderItem(
                    oi.Id,
                    oi.GoodsId,
                    oi.Amount)
                )
            )
        );

        await _sender.InvokeAsync(command);
    }
}