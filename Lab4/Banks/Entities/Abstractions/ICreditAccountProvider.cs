using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Models;

namespace Banks.Entities.Abstractions;

public interface ICreditAccountProvider
{
    IUnchangeableBankAccount CreateCreditAccount(IAccountType type, ICustomer customer, MoneyAmount? balance = null);
}