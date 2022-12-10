namespace Banks.AccountTypes.Abstractions;

public interface IFixedInterestOnBalanceSize : IInterestCalculationAccountType
{
    void SetInterestPercent(decimal interestPercent);
}