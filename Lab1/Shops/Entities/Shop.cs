using System.Diagnostics.CodeAnalysis;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop : IEquatable<Shop>
{
    private readonly BankAccount _bankAccount;
    private readonly ProductInventory _inventory;

    public Shop(string name, Address address, BankAccount bankAccount)
        : this(name, address, new ProductInventory(), bankAccount)
    {
    }

    public Shop(string name, Address address, ProductInventory inventory, BankAccount bankAccount)
    {
        if (string.IsNullOrEmpty(name))
            throw ShopException.EmptyNameException();

        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        _bankAccount = bankAccount;

        if (!inventory.HasMatchingCurrency(_bankAccount.Balance.CurrencySign))
            throw ShopException.MismatchingCurrenciesException(Id, _bankAccount.Balance.CurrencySign);

        _inventory = inventory;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Address Address { get; }
    public IEnumerable<Product> Products => _inventory.Products;

    public ShoppingList SellProducts(Buyer customer, ShoppingList shoppingList)
    {
        MoneyAmount costOfProducts = GetCost(shoppingList);
        customer.SpendMoneyInShop(costOfProducts, _bankAccount);
        ExtractGoodsFromShoppingList(shoppingList);

        return shoppingList;
    }

    public void AddSupply(ProductSupply supply)
    {
        if (!supply.HasMatchingCurrency(_bankAccount.Balance.CurrencySign))
            throw ShopException.MismatchingCurrenciesException(Id, _bankAccount.Balance.CurrencySign);

        foreach (ProductSupplyInformation info in supply.Items)
        {
            ShopItem newItem = GetShopItemFromSupplyInfo(info);
            _inventory.AddItem(newItem);
        }
    }

    public void ChangePrice(Product product, MoneyAmount newPrice)
    {
        if (_bankAccount.Balance.HasDifferentCurrency(newPrice))
            throw ShopException.MismatchingCurrenciesException(Id, _bankAccount.Balance.CurrencySign);

        ShopItem item = _inventory.GetItemByProduct(product);
        item.SetPrice(newPrice);
    }

    public MoneyAmount GetPrice(Product product)
    {
        return _inventory
            .GetItemByProduct(product)
            .Price;
    }

    public MoneyAmount GetCost(ShoppingList shoppingList)
    {
        if (!TryGetCost(shoppingList, out MoneyAmount? cost))
            throw ShopException.NotEnoughItemsException(Id);

        return (MoneyAmount)cost;
    }

    public bool TryGetCost(ShoppingList shoppingList, [NotNullWhen(true)] out MoneyAmount? cost)
    {
        cost = CalculateCostIfPossible(shoppingList);
        return cost is not null;
    }

    public ProductAmount GetAmount(Product product)
    {
        return _inventory
            .GetItemByProduct(product)
            .Amount;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Shop);
    }

    public bool Equals(Shop? other)
    {
        return other is not null
               && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    private MoneyAmount? CalculateCostIfPossible(ShoppingList shoppingList)
    {
        char currencySign = _bankAccount.Balance.CurrencySign;
        var cost = new MoneyAmount(0, currencySign);

        foreach (ShoppingListItem shoppingListItem in shoppingList.Items)
        {
            ShopItem shopItem = _inventory.GetItemByProduct(shoppingListItem.Product);
            if (shopItem.Amount < shoppingListItem.Amount)
                return null;

            decimal itemCost = shopItem.Price.Value * shoppingListItem.Amount.Value;
            cost += new MoneyAmount(itemCost, currencySign);
        }

        return cost;
    }

    private void ExtractGoodsFromShoppingList(ShoppingList shoppingList)
    {
        foreach (ShoppingListItem shoppingListItem in shoppingList.Items)
        {
            ShopItem shopItem = _inventory.GetItemByProduct(shoppingListItem.Product);
            shopItem.DecreaseAmount(shoppingListItem.Amount);
        }
    }

    private ShopItem GetShopItemFromSupplyInfo(ProductSupplyInformation info)
    {
        MoneyAmount newPrice = info.KeepOldPrice ? GetPrice(info.Product) : info.NewPrice;
        return new ShopItem(info.Product, info.Amount, newPrice);
    }
}