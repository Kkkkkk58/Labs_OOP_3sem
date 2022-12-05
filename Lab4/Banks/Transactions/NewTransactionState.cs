using Banks.Commands.Abstractions;
using Banks.Exceptions;
using Banks.Transactions.Abstractions;

namespace Banks.Transactions;

public sealed class NewTransactionState : TransactionState
{
    public NewTransactionState(ITransaction transaction)
        : base(transaction)
    {
        if (transaction.Information.IsCompleted)
            throw TransactionStateException.InvalidTransactionState();
    }

    public override void Perform(ICommand command)
    {
        Transaction.ChangeState(new RunningTransactionState(Transaction));
        Transaction.Information.BankAccount.ExecuteCommand(command, Transaction);
    }

    public override void Cancel(ICommand command)
    {
        throw TransactionStateException.CancellingNewOperation(command.Id);
    }
}