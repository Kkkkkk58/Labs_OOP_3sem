namespace Banks.Models.AccountTypes.Abstractions;

public interface IDebtLimitedAccountType
{
    MoneyAmount DebtLimit { get; }
    void SetDebtLimit(MoneyAmount debtLimit);
}