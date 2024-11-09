using Packing.Domain.Common;

namespace Packing.Domain.Order.DomainEvents;

public record OrderPackagedEvent(Guid Id, Order Order) : DomainEvent(Id);