using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities.Abstractions;

public interface IDebitTypeProvider : ISuspiciousLimitingTypeProvider, IInterestCalculatingTypeProvider
{
    IDebitAccountType CreateDebitAccountType(decimal interestOnBalance, TimeSpan interestCalculationPeriod);
    void ChangeInterestOnBalance(Guid debitTypeId, decimal newInterestOnBalance);
}