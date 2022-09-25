using Shops.Exceptions;

namespace Shops.Entities;

public class Product : IEquatable<Product>
{
    public Product(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ProductException.EmptyNameException();

        Id = Guid.NewGuid();
        Name = name.ToUpperInvariant();
        Description = description;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string? Description { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Product);
    }

    public bool Equals(Product? other)
    {
        return other is not null
               && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}