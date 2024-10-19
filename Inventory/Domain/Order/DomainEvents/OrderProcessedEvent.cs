using Inventory.Domain.Common;

namespace Inventory.Domain.Order.DomainEvents;

public record OrderProcessedEvent(Guid Id, Order Order) : DomainEvent(Id);