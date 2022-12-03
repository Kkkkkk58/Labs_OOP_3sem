using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public sealed class RunningTransactionState : TransactionState
{
    public RunningTransactionState(ITransaction transaction)
        : base(transaction)
    {
        if (transaction.Information.IsCompleted)
            throw new NotImplementedException();
    }

    public override void Perform(ICommand command)
    {
        throw new NotImplementedException();
    }

    public override void Cancel(ICommand command)
    {
        Transaction.ChangeState(new FailedTransactionState(Transaction));
    }
}