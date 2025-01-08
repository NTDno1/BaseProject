using DemoCICD.Domain.Abstractions.Entities;

namespace DemoCICD.Domain.Entities;
public class Product : DomainEntity<Guid>
{
    public string Name { get; private set; }

    public decimal Price { get; private set; }

    public string Description { get; private set; }

    public Product()
    {

    }

    public Product(Guid id, string name, decimal price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public void Update(string? name, decimal? price, string? description)
    {
        Name = name ?? Name;
        Price = price ?? Price;
        Description = description ?? Description;
    }

    public static Product CreateProduct(Guid id, string name, decimal price, string description)
    {
        return new Product(id, name, price, description);
    }
}
