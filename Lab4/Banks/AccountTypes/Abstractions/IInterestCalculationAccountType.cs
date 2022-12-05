using Banks.Models;

namespace Banks.AccountTypes.Abstractions;

public interface IInterestCalculationAccountType : IAccountType
{
    TimeSpan InterestCalculationPeriod { get; }
    void SetInterestCalculationPeriod(TimeSpan interestCalculationPeriod);
    decimal GetInterestPercent(MoneyAmount initialBalance);
}