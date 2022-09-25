namespace Shops.Exceptions;

public class MoneyAmountException : ShopsException
{
    private MoneyAmountException(string message)
        : base(message)
    {
    }

    public static MoneyAmountException NegativeAmountException(decimal value)
    {
        throw new MoneyAmountException($"Can't convert negative value {value} to money amount");
    }

    public static MoneyAmountException DifferentCurrenciesException(char currencySign, char otherCurrencySign)
    {
        throw new MoneyAmountException(
            $"Can't perform operations with different currencies: {currencySign} and {otherCurrencySign}");
    }
}