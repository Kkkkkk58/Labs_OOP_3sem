using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.BankAccounts.Abstractions;

public interface ICommandExecutingBankAccount : IUnchangeableBankAccount
{
    void ExecuteCommand(ICommand command, ITransaction transaction);
}