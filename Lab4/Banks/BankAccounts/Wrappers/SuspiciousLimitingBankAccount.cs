using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Wrappers;

public class SuspiciousLimitingBankAccount : BaseBankAccountWrapper
{
    private readonly IBankAccount _wrapped;
    private readonly ISuspiciousLimitingAccountType _type;

    public SuspiciousLimitingBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
        ArgumentNullException.ThrowIfNull(wrapped);
        if (wrapped.Type is not ISuspiciousLimitingAccountType type)
            throw AccountWrapperException.InvalidWrappedType();

        _type = type;
        _wrapped = wrapped;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        ValidateTransaction(transaction);

        return base.Withdraw(transaction);
    }

    public override void Replenish(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        ValidateTransaction(transaction);

        base.Replenish(transaction);
    }

    private void ValidateTransaction(ITransaction transaction)
    {
        if (SuspiciousAccountLimitExceeded(transaction.Information.OperatedAmount))
        {
            throw TransactionException.SuspiciousOperationsLimitExceeded(
                transaction.Information.OperatedAmount,
                _type.SuspiciousAccountsOperationsLimit);
        }
    }

    private bool SuspiciousAccountLimitExceeded(MoneyAmount moneyAmount)
    {
        return _wrapped.IsSuspicious && moneyAmount > _type.SuspiciousAccountsOperationsLimit;
    }
}