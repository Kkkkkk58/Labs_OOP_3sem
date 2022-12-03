using Banks.Models;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities.Abstractions;

public interface ICreditTypeProvider : ISuspiciousLimitingTypeProvider
{
    ICreditAccountType CreateCreditAccountType(MoneyAmount debtLimit, MoneyAmount charge);
    void ChangeDebtLimit(Guid creditTypeId, MoneyAmount newCreditLimit);
    void ChangeCreditCharge(Guid creditTypeId, MoneyAmount newCharge);
}