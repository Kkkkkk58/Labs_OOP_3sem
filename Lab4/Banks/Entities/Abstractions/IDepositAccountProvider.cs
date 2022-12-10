using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Models;

namespace Banks.Entities.Abstractions;

public interface IDepositAccountProvider
{
    IUnchangeableBankAccount CreateDepositAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null);
}