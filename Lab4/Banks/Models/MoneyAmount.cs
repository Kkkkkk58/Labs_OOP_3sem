using Banks.Exceptions;

namespace Banks.Models;

public readonly record struct MoneyAmount : IComparable<MoneyAmount>
{
    public MoneyAmount(decimal value, char currencySign = '$')
    {
        if (value < 0)
            throw MoneyAmountException.NegativeAmountException(value);

        Value = value;
        CurrencySign = currencySign;
    }

    public char CurrencySign { get; }
    public decimal Value { get; }

    public static MoneyAmount operator +(MoneyAmount lhs, MoneyAmount rhs) => lhs.Add(rhs);
    public static MoneyAmount operator -(MoneyAmount lhs, MoneyAmount rhs) => lhs.Subtract(rhs);
    public static MoneyAmount operator -(MoneyAmount moneyAmount) => moneyAmount.Negative();
    public static MoneyAmount operator *(MoneyAmount lhs, decimal rhs) => lhs.Multiply(rhs);
    public static MoneyAmount operator *(decimal lhs, MoneyAmount rhs) => rhs * lhs;

    public static bool operator <(MoneyAmount lhs, MoneyAmount rhs)
    {
        return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator >(MoneyAmount lhs, MoneyAmount rhs)
    {
        return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator <=(MoneyAmount lhs, MoneyAmount rhs)
    {
        return !(lhs > rhs);
    }

    public static bool operator >=(MoneyAmount lhs, MoneyAmount rhs)
    {
        return !(lhs < rhs);
    }

    public int CompareTo(MoneyAmount other)
    {
        if (HasDifferentCurrency(other))
            throw MoneyAmountException.DifferentCurrenciesException(CurrencySign, other.CurrencySign);

        return Value.CompareTo(other.Value);
    }

    public bool HasDifferentCurrency(MoneyAmount other)
    {
        return !CurrencySign.Equals(other.CurrencySign);
    }

    public override string ToString()
    {
        return $"{CurrencySign}{Value}";
    }

    private MoneyAmount Add(MoneyAmount moneyAmount)
    {
        ValidateCurrencies(moneyAmount);
        return new MoneyAmount(Value + moneyAmount.Value, CurrencySign);
    }

    private MoneyAmount Subtract(MoneyAmount moneyAmount)
    {
        ValidateCurrencies(moneyAmount);
        return new MoneyAmount(Value - moneyAmount.Value, CurrencySign);
    }

    private MoneyAmount Multiply(decimal value)
    {
        return new MoneyAmount(Value * value, CurrencySign);
    }

    private MoneyAmount Negative()
    {
        return new MoneyAmount(-Value, CurrencySign);
    }

    private void ValidateCurrencies(MoneyAmount other)
    {
        if (HasDifferentCurrency(other))
            throw MoneyAmountException.DifferentCurrenciesException(CurrencySign, other.CurrencySign);
    }
}