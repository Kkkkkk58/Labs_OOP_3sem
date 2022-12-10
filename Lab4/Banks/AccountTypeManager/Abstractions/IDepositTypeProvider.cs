using Banks.AccountTypes.Abstractions;
using Banks.Models;

namespace Banks.AccountTypeManager.Abstractions;

public interface IDepositTypeProvider : ISuspiciousLimitingTypeProvider, IInterestCalculatingTypeProvider
{
    IDepositAccountType CreateDepositAccountType(
        TimeSpan depositTerm,
        InterestOnBalancePolicy interestOnBalancePolicy,
        TimeSpan interestCalculationPeriod);

    void ChangeInterestOnBalancePolicy(Guid depositTypeId, InterestOnBalancePolicy newPolicy);
    void ChangeDepositTerm(Guid depositTypeId, TimeSpan newDepositTerm);
}