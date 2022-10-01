using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ShoppingList
{
    private readonly Dictionary<Product, ShoppingListItem> _items;

    public ShoppingList()
        : this(new Dictionary<Product, ShoppingListItem>())
    {
    }

    public ShoppingList(Dictionary<Product, ShoppingListItem> items)
    {
        if (items.Any(kvp => !kvp.Key.Equals(kvp.Value.Product)))
            throw ShoppingListException.MismatchInProductsKvpException();

        _items = items;
    }

    public IReadOnlyCollection<ShoppingListItem> Items => _items.Values;

    public void AddItem(ShoppingListItem item)
    {
        if (_items.TryAdd(item.Product, item))
            return;

        ShoppingListItem oldItem = _items[item.Product];
        oldItem.SetAmount(oldItem.Amount + item.Amount);
    }

    public void RemoveItem(Product product)
    {
        if (!_items.Remove(product))
            throw ShoppingListException.ItemNotFoundException(product);
    }
}