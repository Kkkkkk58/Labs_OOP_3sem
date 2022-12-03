using Banks.Models;

namespace Banks.Entities.Abstractions;

public interface ISuspiciousLimitingTypeProvider
{
    MoneyAmount SuspiciousAccountsOperationsLimit { get; }
    void SetSuspiciousOperationsLimit(MoneyAmount limit);
}