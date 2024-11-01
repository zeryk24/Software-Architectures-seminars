using Packing.Domain.Box.ValueObjects;
using Packing.Domain.Common;

namespace Packing.Domain.Box;

public class Box : AggregateRoot<BoxId>
{
    public BoxOrder Order { get; private set; }

    private Box(){}
    
    private Box(BoxId id, BoxOrder order) : base(id)
    {
        Order = order;
    }

    public static Box Create(BoxId id) => new (id, BoxOrder.Empty());

    public Box PackageOrder(BoxOrder order)
    {
        Order = order;
        return this;
    }
}