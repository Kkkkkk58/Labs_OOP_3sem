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
        var productSupplyInformation =
            new ProductSupplyInformation(product, new ProductAmount(10), new MoneyAmount(12));
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
        CreateSupplyInShop(shop, product, initialProductAmount, price);

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
        CreateSupplyInShop(shop, product, initialProductAmount, price);

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
        var productAmount = new ProductAmount(200);
        var price = new MoneyAmount(5);
        CreateSupplyInShop(shop, product, productAmount, price);

        var newPrice = new MoneyAmount(10000);
        shop.ChangePrice(product, newPrice);

        Assert.Equal(newPrice, shop.GetPrice(product));
    }

    [Fact]
    public void AddSupplyWithNewPrice_PriceChanged()
    {
        Shop shop = _shopService.CreateShop("Walmart", new Address("2960 Kingsway Dr"));
        Product product = _shopService.RegisterProduct("Reese's 202");
        var productAmount = new ProductAmount(200);
        var price = new MoneyAmount(5);
        CreateSupplyInShop(shop, product, productAmount, price);

        var newPrice = new MoneyAmount(10000);
        CreateSupplyInShop(shop, product, productAmount, newPrice);

        Assert.Equal(newPrice, shop.GetPrice(product));
    }

    [Fact]
    public void SearchingTheCheapestShopIfExists_ReturnCheapestShop()
    {
        Product product = _shopService.RegisterProduct("Milka Strawberry Cheesecake");
        var amount = new ProductAmount(15);

        Shop cheapShop = _shopService.CreateShop("Sainsbury's", new Address("Brooks Rd"));
        var cheapPrice = new MoneyAmount(2);
        CreateSupplyInShop(cheapShop, product, amount, cheapPrice);

        Shop expensiveShop = _shopService.CreateShop("Tesco Superstore", new Address("172 East Rd"));
        var expensivePrice = new MoneyAmount(5);
        CreateSupplyInShop(expensiveShop, product, amount, expensivePrice);

        var shoppingList = new ShoppingList();
        shoppingList.AddItem(new ShoppingListItem(product, amount));

        Assert.Equal(cheapShop, _shopService.GetCheapestShop(shoppingList));
        Assert.Equal(cheapShop, _shopService.FindCheapestShop(shoppingList));
    }

    [Fact]
    public void SearchingTheCheapestShopIfDoesNotExist_ErrorHandled()
    {
        Product product = _shopService.RegisterProduct("Milka Strawberry Cheesecake");
        var insufficientAmount = new ProductAmount(1);
        Shop shop = _shopService.CreateShop("Sainsbury's", new Address("Brooks Rd"));
        CreateSupplyInShop(shop, product, insufficientAmount, new MoneyAmount(11));

        var desiredAmount = new ProductAmount(99);
        var shoppingList = new ShoppingList();
        shoppingList.AddItem(new ShoppingListItem(product, desiredAmount));

        Assert.Throws<ShopServiceException>(() => _shopService.GetCheapestShop(shoppingList));
        Assert.Null(_shopService.FindCheapestShop(shoppingList));
    }

    [Fact]
    public void FindShopsByName_ReturnAllShopsWithName()
    {
        const string commonName = "Perekrestok";

        Shop shop1 = _shopService.CreateShop(commonName, new Address("Plekhanova 19"));
        Shop shop2 = _shopService.CreateShop(commonName, new Address("Suvorova 144a"));
        _shopService.CreateShop("Pitnica", new Address("Moskovskaya 65a"));

        IReadOnlyCollection<Shop> suitableShops = _shopService.FindShops(commonName).ToList();

        Assert.True(suitableShops.All(shop => shop.Name.Equals(commonName)));
        Assert.Contains(shop1, suitableShops);
        Assert.Contains(shop2, suitableShops);
    }

    [Fact]
    public void FindShopsByAddress_ReturnAllShopsWithAddress()
    {
        var commonAddress = new Address("Vyazemskii pereulok 5/7");

        Shop shop1 = _shopService.CreateShop("50r04ka", commonAddress);
        Shop shop2 = _shopService.CreateShop("DickSee", commonAddress);
        _shopService.CreateShop("ITMO Store", new Address("Kronverkskiy prospekt 49"));

        IReadOnlyCollection<Shop> suitableShops = _shopService.FindShops(commonAddress).ToList();

        Assert.True(suitableShops.All(shop => shop.Address.Equals(commonAddress)));
        Assert.Contains(shop1, suitableShops);
        Assert.Contains(shop2, suitableShops);
    }

    private static void CreateSupplyInShop(Shop shop, Product product, ProductAmount amount, MoneyAmount price)
    {
        var supply = new ProductSupply();
        var supplyItem = new ProductSupplyInformation(product, amount, price);
        supply.AddItem(supplyItem);
        shop.AddSupply(supply);
    }
}