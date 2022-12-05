using Banks.AccountTypes.Abstractions;
using Banks.Models;

namespace Banks.AccountTypeManager.Abstractions;

public interface ICreditTypeProvider : ISuspiciousLimitingTypeProvider
{
    ICreditAccountType CreateCreditAccountType(MoneyAmount debtLimit, MoneyAmount charge);
    void ChangeDebtLimit(Guid creditTypeId, MoneyAmount newCreditLimit);
    void ChangeCreditCharge(Guid creditTypeId, MoneyAmount newCharge);
}