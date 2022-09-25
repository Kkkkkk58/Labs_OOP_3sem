using Shops.Entities;

namespace Shops.Models;

public class ShoppingListItem
{
    public ShoppingListItem(Product product, ProductAmount amount)
    {
        Product = product;
        Amount = amount;
    }

    public Product Product { get; }
    public ProductAmount Amount { get; private set; }

    public void SetAmount(ProductAmount value)
    {
        Amount = value;
    }

    public override string ToString()
    {
        return $"{Product} - {Amount}";
    }
}