using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Wrappers;

public class DebtLimitedBankAccount : BaseBankAccountWrapper
{
    private readonly IDebtLimitedAccountType _type;

    public DebtLimitedBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
        ArgumentNullException.ThrowIfNull(wrapped);
        if (wrapped.Type is not IDebtLimitedAccountType type)
            throw AccountWrapperException.InvalidWrappedType();

        _type = type;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;
        MoneyAmount debt = EstimateNewDebtValue(moneyAmount);

        if (debt >= _type.DebtLimit)
            throw TransactionException.DebtIsOverTheLimit(debt, _type.DebtLimit);

        return base.Withdraw(transaction);
    }

    private MoneyAmount EstimateNewDebtValue(MoneyAmount moneyAmount)
    {
        var debt = new MoneyAmount(0, _type.DebtLimit.CurrencySign);
        if (Debt.Value != 0)
        {
            debt = Debt + moneyAmount;
        }
        else if (Balance < moneyAmount)
        {
            debt = moneyAmount - Balance;
        }

        return debt;
    }
}