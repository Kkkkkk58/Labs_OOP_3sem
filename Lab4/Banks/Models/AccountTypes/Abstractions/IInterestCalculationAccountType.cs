namespace Banks.Models.AccountTypes.Abstractions;

public interface IInterestCalculationAccountType : IAccountType
{
    TimeSpan InterestCalculationPeriod { get; }
    void SetInterestCalculationPeriod(TimeSpan interestCalculationPeriod);
    decimal GetInterestPercent(MoneyAmount initialBalance);
}