using Banks.Models;

namespace Banks.AccountTypes.Abstractions;

public interface IInterestGradesAccountType : IInterestCalculationAccountType
{
    InterestOnBalancePolicy InterestOnBalancePolicy { get; }
    void SetInterestOnBalancePolicy(InterestOnBalancePolicy interestOnBalancePolicy);
}