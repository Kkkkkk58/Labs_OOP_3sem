using Banks.BankAccounts.Abstractions;

namespace Banks.Entities.Abstractions;

public interface IBank : INoTransactionalBank
{
    ICommandExecutingBankAccount GetExecutingAccount(Guid id);
}