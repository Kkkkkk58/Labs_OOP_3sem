using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Commands;

public class TransferCommand : ICommand
{
    private readonly ICommandExecutingBankAccount _receiver;

    public TransferCommand(ICommandExecutingBankAccount receiver)
    {
        Id = Guid.NewGuid();
        _receiver = receiver;
    }

    public Guid Id { get; }

    public void Execute(IBankAccount bankAccount, ITransaction transaction)
    {
        bankAccount.Withdraw(transaction);
        _receiver.ExecuteCommand(new ReplenishmentCommand(), transaction);
    }

    public void Undo(ITransaction transaction)
    {
        _receiver.ExecuteCommand(new TransferCommand(transaction.Information.BankAccount), transaction);
    }
}