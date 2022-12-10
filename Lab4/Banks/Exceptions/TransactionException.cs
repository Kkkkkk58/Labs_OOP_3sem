using Banks.Models;

namespace Banks.Exceptions;

public class TransactionException : BanksException
{
    private TransactionException(string message)
        : base(message)
    {
    }

    public static TransactionException ChargeRateExceedsMoneyAmount(MoneyAmount moneyAmount, MoneyAmount charge)
    {
        return new TransactionException($"Charge {charge} exceeds transaction money amount {moneyAmount}");
    }

    public static TransactionException DebtIsOverTheLimit(MoneyAmount debt, MoneyAmount debtLimit)
    {
        return new TransactionException(
            $"Debt {debt} value is over the limited debt {debtLimit} value after the transaction");
    }

    public static TransactionException NegativeBalance()
    {
        return new TransactionException("Can't perform this transaction because the balance would be negative");
    }

    public static TransactionException SuspiciousOperationsLimitExceeded(
        MoneyAmount moneyAmount,
        MoneyAmount suspiciousAccountsOperationsLimit)
    {
        return new TransactionException(
            $"Suspicious operation limit is exceeded: was {moneyAmount}, limit is {suspiciousAccountsOperationsLimit}");
    }

    public static TransactionException WithdrawnBeforeLimit()
    {
        return new TransactionException("Can't withdraw money until the time limit is reached");
    }
}