using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ProductInventory
{
    private readonly Dictionary<Product, ShopItem> _items;

    public ProductInventory()
        : this(new Dictionary<Product, ShopItem>())
    {
    }

    public ProductInventory(Dictionary<Product, ShopItem> items)
    {
        ValidateItems(items);

        _items = items;
    }

    public IEnumerable<Product> Products => _items.Values.Select(i => i.Product);

    public void AddItem(ShopItem item)
    {
        if (_items.TryAdd(item.Product, item))
            return;

        ShopItem oldItem = GetItemByProduct(item.Product);
        CombineItemWithExistingOne(oldItem, item);
    }

    public void RemoveItem(Product product)
    {
        if (!_items.Remove(product))
            throw ProductInventoryException.ItemNotFoundException(product);
    }

    public ShopItem GetItemByProduct(Product product)
    {
        ShopItem? itemFoundByProduct = FindItemByProduct(product);
        return itemFoundByProduct ?? throw ProductInventoryException.ItemNotFoundException(product);
    }

    public ShopItem? FindItemByProduct(Product product)
    {
        _items.TryGetValue(product, out ShopItem? item);
        return item;
    }

    public bool HasMatchingCurrency(char shopCurrencySign)
    {
        return _items.Values.All(item => item.Price.CurrencySign.Equals(shopCurrencySign));
    }

    private static void CombineItemWithExistingOne(ShopItem existingItem, ShopItem newItem)
    {
        existingItem.IncreaseAmount(newItem.Amount);
        existingItem.SetPrice(newItem.Price);
    }

    private static void ValidateItems(Dictionary<Product, ShopItem> items)
    {
        if (items.Any(item => !item.Key.Equals(item.Value.Product)))
            throw ProductInventoryException.MismatchInProductsKvpException();
    }
}