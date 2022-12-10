using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Transactions.Abstractions;

namespace Banks.Commands;

public class TransferCommand : Command
{
    private readonly ICommandExecutingBankAccount _receiver;

    public TransferCommand(ICommandExecutingBankAccount receiver)
    {
        _receiver = receiver;
    }

    public override void Execute(IBankAccount bankAccount, ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);
        ArgumentNullException.ThrowIfNull(transaction);

        bankAccount.Withdraw(transaction);
        _receiver.ExecuteCommand(new ReplenishmentCommand(), transaction);
    }

    public override void Undo(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        _receiver.ExecuteCommand(new TransferCommand(transaction.Information.BankAccount), transaction);
    }
}