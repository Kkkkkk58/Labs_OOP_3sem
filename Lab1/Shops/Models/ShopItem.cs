using Shops.Entities;

namespace Shops.Models;

public record ShopItem
{
    public ShopItem(Product product, ProductAmount amount, MoneyAmount price)
    {
        Product = product;
        Amount = amount;
        Price = price;
    }

    public Product Product { get; }
    public ProductAmount Amount { get; private set; }
    public MoneyAmount Price { get; private set; }

    public void IncreaseAmount(ProductAmount amountToAdd)
    {
        Amount += amountToAdd;
    }

    public void DecreaseAmount(ProductAmount amountToSubtract)
    {
        Amount -= amountToSubtract;
    }

    public void SetPrice(MoneyAmount newPrice)
    {
        Price = newPrice;
    }

    public override string ToString()
    {
        return $"{Product} - {Price} x {Amount}";
    }
}