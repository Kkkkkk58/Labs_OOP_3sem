using Banks.AccountTypes.Abstractions;

namespace Banks.AccountTypeManager.Abstractions;

public interface IDebitTypeProvider : ISuspiciousLimitingTypeProvider, IInterestCalculatingTypeProvider
{
    IDebitAccountType CreateDebitAccountType(decimal interestOnBalance, TimeSpan interestCalculationPeriod);
    void ChangeInterestOnBalance(Guid debitTypeId, decimal newInterestOnBalance);
}