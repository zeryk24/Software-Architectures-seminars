using FoodDelivery.Contracts;
using Marten;

namespace EventLog.Order;

public class OrderCreatedHandler(IDocumentSession session)
{
    public async Task Handle(OrderCompletedMessage message)
    {
        session.Events.StartStream(message.CompletedOrder.OrderId, message);
        
        await session.SaveChangesAsync();
    }
}