using DemoCICD.Domain.Abstractions.Entities;

namespace DemoCICD.Domain.Entities.Identity;
public class Product : DomainEntity<Guid>
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }
}
