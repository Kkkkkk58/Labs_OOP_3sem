using Banks.Models;

namespace Banks.AccountTypes.Abstractions;

public abstract class SuspiciousLimitingAccountType : ISuspiciousLimitingAccountType
{
    protected SuspiciousLimitingAccountType(MoneyAmount suspiciousAccountsOperationsLimit)
    {
        Id = Guid.NewGuid();
        SuspiciousAccountsOperationsLimit = suspiciousAccountsOperationsLimit;
    }

    public Guid Id { get; }
    public MoneyAmount SuspiciousAccountsOperationsLimit { get; private set; }

    public void SetSuspiciousAccountsOperationsLimit(MoneyAmount limit)
    {
        SuspiciousAccountsOperationsLimit = limit;
    }
}