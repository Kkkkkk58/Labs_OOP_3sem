using Banks.BankAccounts.Abstractions;
using Banks.Models;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities.Abstractions;

public interface IDepositAccountProvider
{
    IUnchangeableBankAccount CreateDepositAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null);
}