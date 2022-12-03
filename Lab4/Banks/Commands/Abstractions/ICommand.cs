using Banks.BankAccounts.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Commands.Abstractions;

public interface ICommand
{
    Guid Id { get; }
    void Execute(IBankAccount bankAccount, ITransaction transaction);
    void Undo(ITransaction transaction);
}