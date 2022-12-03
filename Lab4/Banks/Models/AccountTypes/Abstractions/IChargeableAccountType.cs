namespace Banks.Models.AccountTypes.Abstractions;

public interface IChargeableAccountType : IAccountType
{
    MoneyAmount Charge { get; }
    void SetCharge(MoneyAmount charge);
}