using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Tools.Abstractions;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Wrappers;

public class TimeLimitedWithdrawalBankAccount : BaseBankAccountWrapper
{
    private readonly ITimeLimitedWithdrawalAccountType _type;
    private readonly IClock _clock;

    public TimeLimitedWithdrawalBankAccount(IBankAccount wrapped, IClock clock)
        : base(wrapped)
    {
        ArgumentNullException.ThrowIfNull(wrapped);
        if (wrapped.Type is not ITimeLimitedWithdrawalAccountType type)
            throw AccountWrapperException.InvalidWrappedType();

        _type = type;
        _clock = clock;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        if (IsWithdrawnBeforeLimit())
            throw TransactionException.WithdrawnBeforeLimit();

        return base.Withdraw(transaction);
    }

    private bool IsWithdrawnBeforeLimit()
    {
        return CreationDate + _type.DepositTerm < _clock.Now;
    }
}