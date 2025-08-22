namespace PostBinar.Domain.Abstraction;

public abstract class Entity<TEntityId>
{
    protected Entity(TEntityId id)
    {
        this.Id = id;
    }

    protected Entity() { this.Id = default!; } //EF core

    public TEntityId Id { get; protected init; }
}