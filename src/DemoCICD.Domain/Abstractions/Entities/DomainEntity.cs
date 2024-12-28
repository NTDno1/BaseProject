namespace DemoCICD.Domain.Abstractions.Entities;
public abstract class DomainEntity<Tkey>
{
    public virtual Tkey Id { get; set; }

    public bool IsTransient()
    {
        return Id.Equals(default(Tkey));
    }
}
