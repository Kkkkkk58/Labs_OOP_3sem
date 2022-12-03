using Banks.BankAccounts.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.BankAccountWrappers;

public class TimeLimitedWithdrawalBankAccount : BaseBankAccountWrapper
{
    private readonly ITimeLimitedWithdrawalAccountType _type;
    private readonly IClock _clock;

    public TimeLimitedWithdrawalBankAccount(IBankAccount wrapped, IClock clock)
        : base(wrapped)
    {
        if (wrapped.Type is not ITimeLimitedWithdrawalAccountType type)
            throw new NotImplementedException();

        _type = type;
        _clock = clock;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        if (IsWithdrawnBeforeLimit())
        {
            transaction.Cancel();
            throw new NotImplementedException();
        }

        return base.Withdraw(transaction);
    }

    private bool IsWithdrawnBeforeLimit()
    {
        return CreationDate + _type.DepositTerm < _clock.Now;
    }
}