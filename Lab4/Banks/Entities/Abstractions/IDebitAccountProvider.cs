using Banks.BankAccounts.Abstractions;
using Banks.Models;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Entities.Abstractions;

public interface IDebitAccountProvider
{
    IUnchangeableBankAccount CreateDebitAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null);
}