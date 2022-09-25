using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopServiceTest
{
    private readonly ShopService _shopService;

    public ShopServiceTest()
    {
        var options = new ShopServiceOptions('$');
        _shopService = new ShopService(options);
    }

    [Fact]
    public void AddingProductSupply_ProductsAdded()
    {
        Shop shop = _shopService.CreateShop("Walmart", new Address("2960 Kingsway Dr"));
        Product product = _shopService.RegisterProduct("Reese's 202");
        var productSupplyInformation = new ProductSupplyInformation(product, new ProductAmount(10), new MoneyAmount(12));
        var supply = new ProductSupply();
        supply.AddItem(productSupplyInformation);

        shop.AddSupply(supply);

        Assert.Contains(product, shop.Products);
        Assert.Equal(productSupplyInformation.NewPrice, shop.GetPrice(product));
        Assert.Equal(productSupplyInformation.Amount, shop.GetAmount(product));
    }

    [Fact]
    public void BuyingProductsWithEnoughMoney_TransactionCompleted()
    {
        Shop shop = _shopService.CreateShop("Walmart", new Address("2960 Kingsway Dr"));
        Product product = _shopService.RegisterProduct("Reese's 202");

        var initialProductAmount = new ProductAmount(200);
        var price = new MoneyAmount(5);
        var info = new ProductSupplyInformation(product, initialProductAmount, price);
        var supply = new ProductSupply();
        supply.AddItem(info);
        shop.AddSupply(supply);

        var initialBalance = new MoneyAmount(50);
        var buyer = new Buyer("Artyom Shvetsov", new BankAccount(initialBalance));
        var amountToBuy = new ProductAmount(10);
        var shoppingListItem = new ShoppingListItem(product, amountToBuy);
        var shoppingList = new ShoppingList();
        shoppingList.AddItem(shoppingListItem);

        shop.SellProducts(buyer, shoppingList);

        var buyerExpenditure = new MoneyAmount(price.Value * amountToBuy.Value);
        Assert.Equal(initialProductAmount - amountToBuy, shop.GetAmount(product));
        Assert.Equal(initialBalance - buyerExpenditure, buyer.Balance);
    }

    [Fact]
    public void BuyingProductsNotEnoughMoney_ThrowsException()
    {
        Shop shop = _shopService.CreateShop("Walmart", new Address("2960 Kingsway Dr"));
        Product product = _shopService.RegisterProduct("Reese's 202");

        var initialProductAmount = new ProductAmount(200);
        var price = new MoneyAmount(5);
        var info = new ProductSupplyInformation(product, initialProductAmount, price);
        var supply = new ProductSupply();
        supply.AddItem(info);
        shop.AddSupply(supply);

        var initialBalance = new MoneyAmount(10);
        var buyer = new Buyer("Artyom Shvetsov", new BankAccount(initialBalance));
        var amountToBuy = new ProductAmount(10);
        var shoppingListItem = new ShoppingListItem(product, amountToBuy);
        var shoppingList = new ShoppingList();
        shoppingList.AddItem(shoppingListItem);

        Assert.Throws<BankAccountException>(() => shop.SellProducts(buyer, shoppingList));
    }

    [Fact]
    public void ChangePriceOfProduct_PriceChanged()
    {
        Shop shop = _shopService.CreateShop("Walmart", new Address("2960 Kingsway Dr"));
        Product product = _shopService.RegisterProduct("Reese's 202");

        var initialProductAmount = new ProductAmount(200);
        var price = new MoneyAmount(5);
        var info = new ProductSupplyInformation(product, initialProductAmount, price);
        var supply = new ProductSupply();
        supply.AddItem(info);
        shop.AddSupply(supply);
        var newPrice = new MoneyAmount(10000);
        shop.ChangePrice(product, newPrice);

        Assert.Equal(newPrice, shop.GetPrice(product));
    }

    [Fact]
    public void AddSupplyWithNewPrice_PriceChanged()
    {
        Shop shop = _shopService.CreateShop("Walmart", new Address("2960 Kingsway Dr"));
        Product product = _shopService.RegisterProduct("Reese's 202");

        var initialProductAmount = new ProductAmount(200);
        var price = new MoneyAmount(5);
        var info = new ProductSupplyInformation(product, initialProductAmount, price);
        var supply = new ProductSupply();
        supply.AddItem(info);
        shop.AddSupply(supply);
        var newPrice = new MoneyAmount(10000);
        var newSupply = new ProductSupply();
        newSupply.AddItem(new ProductSupplyInformation(product, new ProductAmount(20), newPrice));
        shop.AddSupply(newSupply);

        Assert.Equal(newPrice, shop.GetPrice(product));
    }

    [Fact]
    public void SearchingTheCheapestShopIfExists_ReturnCheapestShop()
    {
        Product product = _shopService.RegisterProduct("Milka Strawberry Cheesecake");
        var amount = new ProductAmount(15);
        Shop cheapShop = _shopService.CreateShop("Sainsbury's", new Address("Brooks Rd"));
        var cheapPrice = new MoneyAmount(2);
        var cheapSupply = new ProductSupply();
        cheapSupply.AddItem(new ProductSupplyInformation(product, amount, cheapPrice));
        cheapShop.AddSupply(cheapSupply);
        Shop expensiveShop = _shopService.CreateShop("Tesco Superstore", new Address("172 East Rd"));
        var expensivePrice = new MoneyAmount(5);
        var expensiveSupply = new ProductSupply();
        expensiveSupply.AddItem(new ProductSupplyInformation(product, amount, expensivePrice));
        expensiveShop.AddSupply(expensiveSupply);

        var shoppingList = new ShoppingList();
        shoppingList.AddItem(new ShoppingListItem(product, amount));

        Assert.Equal(cheapShop, _shopService.GetCheapestShop(shoppingList));
        Assert.NotNull(_shopService.FindCheapestShop(shoppingList));
    }

    [Fact]
    public void SearchingTheCheapestShopIfDoesNotExist_ErrorHandled()
    {
        Product product = _shopService.RegisterProduct("Milka Strawberry Cheesecake");
        var littleAmount = new ProductAmount(15);
        Shop shop = _shopService.CreateShop("Sainsbury's", new Address("Brooks Rd"));
        var cheapSupply = new ProductSupply();
        cheapSupply.AddItem(new ProductSupplyInformation(product, littleAmount, new MoneyAmount(1)));
        shop.AddSupply(cheapSupply);

        var bigAmount = new ProductAmount(99);
        var shoppingList = new ShoppingList();
        shoppingList.AddItem(new ShoppingListItem(product, bigAmount));

        Assert.Throws<ShopServiceException>(() => _shopService.GetCheapestShop(shoppingList));
        Assert.Null(_shopService.FindCheapestShop(shoppingList));
    }

    [Fact]
    public void FindShopsByName_ReturnAllShopsWithName()
    {
        const string commonName = "Perekrestok";
        _shopService.CreateShop("Pitnica", new Address("Moskovskaya 65a"));
        Shop shop1 = _shopService.CreateShop(commonName, new Address("Plekhanova 19"));
        Shop shop2 = _shopService.CreateShop(commonName, new Address("Suvorova 144a"));

        IReadOnlyCollection<Shop> suitableShops = _shopService.FindShops(commonName).ToList();

        Assert.True(suitableShops.All(shop => shop.Name.Equals(commonName)));
        Assert.Contains(shop1, suitableShops);
        Assert.Contains(shop2, suitableShops);
    }

    [Fact]
    public void FindShopsByAddress_ReturnAllShopsWithAddress()
    {
        var address1 = new Address("Vyazemskii pereulok 5/7");
        var address2 = new Address("Kronverkskiy prospekt 49");

        Shop shop1 = _shopService.CreateShop("50r04ka", address1);
        Shop shop2 = _shopService.CreateShop("DickSee", address1);
        _shopService.CreateShop("ITMO Store", address2);

        IReadOnlyCollection<Shop> suitableShops = _shopService.FindShops(address1).ToList();

        Assert.True(suitableShops.All(shop => shop.Address.Equals(address1)));
        Assert.Contains(shop1, suitableShops);
        Assert.Contains(shop2, suitableShops);
    }
}