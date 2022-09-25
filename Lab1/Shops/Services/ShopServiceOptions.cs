namespace Shops.Services;

public readonly struct ShopServiceOptions
{
    public ShopServiceOptions(char currencySign)
    {
        CurrencySign = currencySign;
    }

    public char CurrencySign { get; }
}