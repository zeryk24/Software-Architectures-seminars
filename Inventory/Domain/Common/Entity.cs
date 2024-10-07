namespace Inventory.Domain.Common;

internal abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; protected set; }

    protected Entity() { }

    protected Entity(TId id)
    {
        if (object.Equals(id, default(TId)))
        {
            throw new ArgumentException("The ID cannot be the default value.", "id");
        }

        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId> one, Entity<TId> two)
    {
        return Equals(one, two);
    }

    public static bool operator !=(Entity<TId> one, Entity<TId> two)
    {
        return !Equals(one, two);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }
}
