using Shops.Exceptions;

namespace Shops.Models;

public readonly record struct ProductAmount : IComparable<ProductAmount>
{
    public ProductAmount(int amount)
    {
        if (amount < 0)
            throw ProductAmountException.NegativeAmountException(amount);

        Value = amount;
    }

    public int Value { get; }

    public static ProductAmount operator +(ProductAmount lhs, ProductAmount rhs) => lhs.Add(rhs);
    public static ProductAmount operator -(ProductAmount lhs, ProductAmount rhs) => lhs.Subtract(rhs);

    public static bool operator <(ProductAmount lhs, ProductAmount rhs)
    {
        return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >(ProductAmount lhs, ProductAmount rhs)
    {
        return lhs.CompareTo(rhs) > 0;
    }

    private ProductAmount Add(ProductAmount other)
    {
        return new ProductAmount(Value + other.Value);
    }

    private ProductAmount Subtract(ProductAmount other)
    {
        return new ProductAmount(Value - other.Value);
    }

    public int CompareTo(ProductAmount other)
    {
        return Value.CompareTo(other.Value);
    }
}