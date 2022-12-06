using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Wrappers;

public class ChargeableAfterDebtBankAccount : BaseBankAccountWrapper
{
    private readonly IChargeableAccountType _type;

    public ChargeableAfterDebtBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
        ArgumentNullException.ThrowIfNull(wrapped);
        if (wrapped.Type is not IChargeableAccountType type)
            throw AccountWrapperException.InvalidWrappedType();

        _type = type;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        ApplyWithdrawalCharge(transaction);

        return base.Withdraw(transaction);
    }

    public override void Replenish(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        ApplyReplenishCharge(transaction);

        base.Replenish(transaction);
    }

    private void ApplyWithdrawalCharge(ITransaction transaction)
    {
        if (Debt.Value == 0)
            return;

        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;
        transaction.Information.SetOperatedAmount(moneyAmount + _type.Charge);
    }

    private void ApplyReplenishCharge(ITransaction transaction)
    {
        if (Debt.Value == 0)
            return;

        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;
        if (moneyAmount < _type.Charge)
            throw TransactionException.ChargeRateExceedsMoneyAmount(moneyAmount, _type.Charge);

        transaction.Information.SetOperatedAmount(moneyAmount - _type.Charge);
    }
}