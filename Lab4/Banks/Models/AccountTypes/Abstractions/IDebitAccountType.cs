namespace Banks.Models.AccountTypes.Abstractions;

public interface IDebitAccountType : IFixedInterestOnBalanceSize, ISuspiciousLimitingAccountType
{
}