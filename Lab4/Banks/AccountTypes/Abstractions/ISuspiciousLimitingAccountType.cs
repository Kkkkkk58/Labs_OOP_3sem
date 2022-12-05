using Banks.Models;

namespace Banks.AccountTypes.Abstractions;

public interface ISuspiciousLimitingAccountType : IAccountType
{
    MoneyAmount SuspiciousAccountsOperationsLimit { get; }
    void SetSuspiciousAccountsOperationsLimit(MoneyAmount limit);
}