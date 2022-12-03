namespace Banks.Models.AccountTypes.Abstractions;

public interface ITimeLimitedWithdrawalAccountType : IAccountType
{
    TimeSpan DepositTerm { get; }
    void SetDepositTerm(TimeSpan depositTerm);
}