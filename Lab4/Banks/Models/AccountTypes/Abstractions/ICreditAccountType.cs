namespace Banks.Models.AccountTypes.Abstractions;

public interface ICreditAccountType : IChargeableAccountType, IDebtLimitedAccountType, ISuspiciousLimitingAccountType
{
}