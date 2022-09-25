using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopService : IShopService
{
    private readonly ShopServiceOptions _shopServiceOptions;
    private readonly List<Shop> _shops;
    private readonly List<Product> _products;

    public ShopService(ShopServiceOptions shopServiceOptions)
        : this(new List<Shop>(), new List<Product>(), shopServiceOptions)
    {
    }

    public ShopService(List<Shop> shops, List<Product> products, ShopServiceOptions shopServiceOptions)
    {
        _shops = shops;
        _products = products;
        _shopServiceOptions = shopServiceOptions;
    }

    public IReadOnlyList<Shop> Shops => _shops;
    public IReadOnlyList<Product> Products => _products;

    public Shop CreateShop(string name, Address address, decimal initialBalance = 0)
    {
        var initialMoneyAmount = new MoneyAmount(initialBalance, _shopServiceOptions.CurrencySign);
        var shopBankAccount = new BankAccount(initialMoneyAmount);
        var shop = new Shop(name, address, shopBankAccount);
        _shops.Add(shop);

        return shop;
    }

    public Shop GetShop(Guid shopId)
    {
        Shop? resultFoundById = FindShop(shopId);
        return resultFoundById ?? throw ShopServiceException.ShopNotFoundException(shopId);
    }

    public Shop? FindShop(Guid shopId)
    {
        return _shops.SingleOrDefault(shop => shop.Id.Equals(shopId));
    }

    public IEnumerable<Shop> FindShops(string name)
    {
        return _shops
            .Where(shop => shop.Name.Equals(name));
    }

    public IEnumerable<Shop> FindShops(Address address)
    {
        return _shops
            .Where(shop => shop.Address.Equals(address));
    }

    public Product RegisterProduct(string name, string? description = null)
    {
        var product = new Product(name, description);
        _products.Add(product);

        return product;
    }

    public Product GetProduct(Guid productId)
    {
        Product? resultFoundById = FindProduct(productId);
        return resultFoundById ?? throw ShopServiceException.ProductNotFoundException(productId);
    }

    public Product? FindProduct(Guid productId)
    {
        return _products.SingleOrDefault(product => product.Id.Equals(productId));
    }

    public Shop? FindCheapestShop(ShoppingList shoppingList)
    {
        Shop? result = null;
        var minCost = new MoneyAmount(decimal.MaxValue, _shopServiceOptions.CurrencySign);
        foreach (Shop shop in _shops)
        {
            if (!shop.TryGetCost(shoppingList, out MoneyAmount cost) || cost >= minCost)
                continue;

            result = shop;
            minCost = cost;
        }

        return result;
    }

    public Shop GetCheapestShop(ShoppingList shoppingList)
    {
        Shop? cheapestShop = FindCheapestShop(shoppingList);
        return cheapestShop ?? throw ShopServiceException.CheapestShopNotFound();
    }
}