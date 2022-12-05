using Banks.Models;

namespace Banks.AccountTypes.Abstractions;

public interface IDebtLimitedAccountType
{
    MoneyAmount DebtLimit { get; }
    void SetDebtLimit(MoneyAmount debtLimit);
}