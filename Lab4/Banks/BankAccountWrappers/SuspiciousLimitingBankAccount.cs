using Banks.BankAccounts.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.BankAccountWrappers;

public class SuspiciousLimitingBankAccount : BaseBankAccountWrapper
{
    private readonly IBankAccount _wrapped;
    private readonly ISuspiciousLimitingAccountType _type;

    public SuspiciousLimitingBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
        if (wrapped.Type is not ISuspiciousLimitingAccountType type)
            throw new NotImplementedException();

        _type = type;
        _wrapped = wrapped;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        if (SuspiciousAccountLimitExceeded(transaction.Information.OperatedAmount))
        {
            transaction.Cancel();
        }

        return base.Withdraw(transaction);
    }

    public override void Replenish(ITransaction transaction)
    {
        if (SuspiciousAccountLimitExceeded(transaction.Information.OperatedAmount))
        {
            transaction.Cancel();
            throw new NotImplementedException();
        }

        base.Replenish(transaction);
    }

    private bool SuspiciousAccountLimitExceeded(MoneyAmount moneyAmount)
    {
        return _wrapped.IsSuspicious && moneyAmount > _type.SuspiciousAccountsOperationsLimit;
    }
}