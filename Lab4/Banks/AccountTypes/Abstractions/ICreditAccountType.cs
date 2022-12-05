namespace Banks.AccountTypes.Abstractions;

public interface ICreditAccountType : IChargeableAccountType, IDebtLimitedAccountType, ISuspiciousLimitingAccountType
{
}