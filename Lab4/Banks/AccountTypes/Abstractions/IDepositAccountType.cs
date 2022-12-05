namespace Banks.AccountTypes.Abstractions;

public interface IDepositAccountType : IInterestGradesAccountType, ITimeLimitedWithdrawalAccountType,
    ISuspiciousLimitingAccountType
{
}