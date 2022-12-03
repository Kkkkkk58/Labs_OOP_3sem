using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities.Abstractions;

public interface IDepositTypeProvider : ISuspiciousLimitingTypeProvider, IInterestCalculatingTypeProvider
{
    IDepositAccountType CreateDepositAccountType(TimeSpan depositTerm, InterestOnBalancePolicy interestOnBalancePolicy, TimeSpan interestCalculationPeriod);
    void ChangeInterestOnBalanceLayer(Guid depositTypeId, Guid layerToSubstituteId, InterestOnBalanceLayer newLayer);
    void ChangeDepositTerm(Guid depositTypeId, TimeSpan newDepositTerm);
}