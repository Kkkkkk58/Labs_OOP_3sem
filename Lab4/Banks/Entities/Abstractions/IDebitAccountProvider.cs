using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Models;

namespace Banks.Entities.Abstractions;

public interface IDebitAccountProvider
{
    IUnchangeableBankAccount CreateDebitAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null);
}