using Banks.Models;

namespace Banks.AccountTypes.Abstractions;

public interface IInterestGradesAccountType : IInterestCalculationAccountType
{
    // TODO Make readonly layers
    InterestOnBalancePolicy InterestOnBalancePolicy { get; }
    InterestOnBalanceLayer AddLayer(InterestOnBalanceLayer layer);
    void RemoveLayer(InterestOnBalanceLayer layer);
}