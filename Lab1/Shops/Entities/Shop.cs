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

        if (HasMismatchingCurrencies(inventory))
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
        if (HasMismatchingCurrencies(supply))
            throw ShopException.MismatchingCurrenciesException(Id, _bankAccount.Balance.CurrencySign);

        foreach (ProductSupplyInformation info in supply.Items)
        {
            ShopItem newItem = GetShopItemFromSupplyInfo(info);
            _inventory.AddItem(newItem);
        }
    }

    public void ChangePrice(Product product, MoneyAmount newPrice)
    {
        if (HasMismatchingCurrency(newPrice))
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
        if (!TryGetCost(shoppingList, out MoneyAmount cost))
            throw ShopException.LackOfItemsException(Id);

        return cost;
    }

    public bool TryGetCost(ShoppingList shoppingList, out MoneyAmount cost)
    {
        char currencySign = _bankAccount.Balance.CurrencySign;
        cost = new MoneyAmount(0, currencySign);

        foreach (ShoppingListItem shoppingListItem in shoppingList.Items)
        {
            ShopItem shopItem = _inventory.GetItemByProduct(shoppingListItem.Product);
            if (shopItem.Amount < shoppingListItem.Amount)
                return false;

            decimal itemCost = shopItem.Price.Value * shoppingListItem.Amount.Value;
            cost += new MoneyAmount(itemCost, currencySign);
        }

        return true;
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

    private bool HasMismatchingCurrencies(ProductInventory inventory)
    {
        return inventory
            .Products
            .Select(inventory.GetItemByProduct)
            .Any(item => _bankAccount.Balance.HasDifferentCurrency(item.Price));
    }

    private bool HasMismatchingCurrencies(ProductSupply supply)
    {
        return supply
            .Items
            .Any(item => !item.KeepOldPrice && _bankAccount.Balance.HasDifferentCurrency(item.NewPrice));
    }

    private bool HasMismatchingCurrency(MoneyAmount amount)
    {
        return _bankAccount.Balance.HasDifferentCurrency(amount);
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

    private bool HasDifferentCurrency(MoneyAmount moneyAmount)
    {
        return _bankAccount.Balance.HasDifferentCurrency(moneyAmount);
    }
}