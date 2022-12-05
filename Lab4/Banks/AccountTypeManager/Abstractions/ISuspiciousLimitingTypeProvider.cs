using Banks.Models;

namespace Banks.AccountTypeManager.Abstractions;

public interface ISuspiciousLimitingTypeProvider
{
    MoneyAmount SuspiciousAccountsOperationsLimit { get; }
    void SetSuspiciousOperationsLimit(MoneyAmount limit);
}