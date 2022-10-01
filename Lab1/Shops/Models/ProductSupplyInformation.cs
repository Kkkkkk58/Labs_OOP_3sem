using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public readonly struct ProductSupplyInformation
{
    private readonly MoneyAmount? _newPrice;

    public ProductSupplyInformation(Product product, ProductAmount amount, MoneyAmount? newPrice = null)
    {
        Product = product;
        Amount = amount;
        _newPrice = newPrice;
    }

    public Product Product { get; }
    public ProductAmount Amount { get; }
    public bool KeepOldPrice => !_newPrice.HasValue;
    public MoneyAmount NewPrice => _newPrice ?? throw ProductSupplyException.NewPriceIsNotSetException();
}