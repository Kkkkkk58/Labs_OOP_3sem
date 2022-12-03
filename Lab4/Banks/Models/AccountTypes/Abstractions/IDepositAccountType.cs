namespace Banks.Models.AccountTypes.Abstractions;

public interface IDepositAccountType : IInterestGradesAccountType, ITimeLimitedWithdrawalAccountType, ISuspiciousLimitingAccountType
{
}