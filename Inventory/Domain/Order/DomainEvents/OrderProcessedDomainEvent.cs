using Inventory.Domain.Common;

namespace Inventory.Domain.Order.DomainEvents;

public record OrderProcessedDomainEvent(Guid Id, Order Order) : DomainEvent(Id), IOrderDomainEvent;