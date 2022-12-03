using Banks.Entities;

namespace Banks.Models.AccountTypes.Abstractions;

public interface IInterestGradesAccountType : IInterestCalculationAccountType
{
    // TODO Make readonly layers
    InterestOnBalancePolicy InterestOnBalancePolicy { get; }
    InterestOnBalanceLayer AddLayer(InterestOnBalanceLayer layer);
    void RemoveLayer(InterestOnBalanceLayer layer);
}