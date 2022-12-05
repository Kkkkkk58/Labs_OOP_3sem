using Banks.Commands.Abstractions;
using Banks.Exceptions;
using Banks.Transactions.Abstractions;

namespace Banks.Transactions;

public sealed class RunningTransactionState : TransactionState
{
    public RunningTransactionState(ITransaction transaction)
        : base(transaction)
    {
        if (transaction.Information.IsCompleted)
            throw TransactionStateException.InvalidTransactionState();
    }

    public override void Perform(ICommand command)
    {
        throw TransactionStateException.AlreadyRunningOperation(command.Id);
    }

    public override void Cancel(ICommand command)
    {
        Transaction.ChangeState(new FailedTransactionState(Transaction));
    }
}