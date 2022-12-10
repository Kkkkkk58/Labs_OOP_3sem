using Banks.AccountTypeManager.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Models;

namespace Banks.Entities.Abstractions;

public interface INoTransactionalBank : IDebitAccountProvider, IDepositAccountProvider, ICreditAccountProvider
{
    Guid Id { get; }
    string Name { get; }
    MoneyAmount SuspiciousAccountsOperationsLimit { get; }
    IAccountTypeManager AccountTypeManager { get; }
    IReadOnlyCollection<ICustomer> Customers { get; }

    ICustomer RegisterCustomer(ICustomer customer);
    void SetSuspiciousAccountsOperationsLimit(MoneyAmount limit);
    IReadOnlyCollection<IUnchangeableBankAccount> GetAccounts(Guid accountHolderId);
    IUnchangeableBankAccount? FindAccount(Guid accountId);
    IUnchangeableBankAccount GetAccount(Guid accountId);
}