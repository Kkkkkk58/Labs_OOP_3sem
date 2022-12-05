using Banks.Models;

namespace Banks.Console.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string s)
    {
        return Guid.Parse(s);
    }

    public static MoneyAmount ToMoneyAmount(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            throw new ArgumentNullException(nameof(s));

        char currencySign = s[0];
        if (char.IsDigit(currencySign))
            return new MoneyAmount(decimal.Parse(s));

        decimal value = decimal.Parse(s[1..]);
        return new MoneyAmount(value, currencySign);
    }
}