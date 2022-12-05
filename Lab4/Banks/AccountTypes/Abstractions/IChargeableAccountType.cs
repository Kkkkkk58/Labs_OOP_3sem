using Banks.Models;

namespace Banks.AccountTypes.Abstractions;

public interface IChargeableAccountType : IAccountType
{
    MoneyAmount Charge { get; }
    void SetCharge(MoneyAmount charge);
}