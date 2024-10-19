using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.Common;

public class BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = [];
    
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}