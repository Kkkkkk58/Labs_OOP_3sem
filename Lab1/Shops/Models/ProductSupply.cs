using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ProductSupply
{
    private readonly Dictionary<Product, ProductSupplyInformation> _items;

    public ProductSupply()
        : this(new Dictionary<Product, ProductSupplyInformation>())
    {
    }

    public ProductSupply(Dictionary<Product, ProductSupplyInformation> items)
    {
        ValidateItems(items);

        _items = items;
    }

    public IReadOnlyCollection<ProductSupplyInformation> Items => _items.Values;

    private static void ValidateItems(Dictionary<Product, ProductSupplyInformation> items)
    {
        if (items.Any(kvp => !kvp.Key.Equals(kvp.Value.Product)))
            throw ProductSupplyException.MismatchInProductsKvpException();
    }
}