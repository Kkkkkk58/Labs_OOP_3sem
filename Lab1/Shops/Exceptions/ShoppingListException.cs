using Shops.Entities;

namespace Shops.Exceptions;

public class ShoppingListException : ShopsException
{
    private ShoppingListException(string message)
        : base(message)
    {
    }

    public static ShoppingListException ItemNotFoundException(Product product)
    {
        return new ShoppingListException($"An item {product} wasn't found");
    }

    public static ShoppingListException MismatchInProductsKvpException()
    {
        return new ShoppingListException("Some product provided as a key was not part of the shopping list item");
    }
}