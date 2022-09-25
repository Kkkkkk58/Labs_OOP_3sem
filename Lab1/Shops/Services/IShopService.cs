using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopService
{
    Shop CreateShop(string name, Address address, decimal initialBalance);
    Shop GetShop(Guid shopId);
    Shop? FindShop(Guid shopId);
    IEnumerable<Shop> FindShops(string name);
    IEnumerable<Shop> FindShops(Address address);

    Product RegisterProduct(string name, string? description);
    Product GetProduct(Guid productId);
    Product? FindProduct(Guid productId);

    Shop? FindCheapestShop(ShoppingList shoppingList);
    Shop GetCheapestShop(ShoppingList shoppingList);
}