using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.BankAccounts.Abstractions;

public interface IUnchangeableBankAccount
{
    Guid Id { get; }
    IAccountType Type { get; }
    ICustomer Holder { get; }
    MoneyAmount Balance { get; }
    MoneyAmount Debt { get; }
    DateTime CreationDate { get; }
    MoneyAmount InitialBalance { get; }
    bool IsSuspicious { get; }
    IReadOnlyCollection<IOperationInformation> OperationHistory { get; }
}