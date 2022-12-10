using Banks.Commands.Abstractions;
using Banks.Transactions.Abstractions;

namespace Banks.Transactions;

public class SuccessfulTransactionState : CompletedTransactionState
{
    public SuccessfulTransactionState(ITransaction transaction)
        : base(transaction)
    {
    }

    public override void Cancel(ICommand command)
    {
        command.Undo(Transaction);
        Transaction.ChangeState(new CancelledTransactionState(Transaction));
    }
}