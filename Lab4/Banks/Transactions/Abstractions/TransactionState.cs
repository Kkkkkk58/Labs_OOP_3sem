using Banks.Commands.Abstractions;

namespace Banks.Transactions.Abstractions;

public abstract class TransactionState : ITransactionState
{
    protected TransactionState(ITransaction transaction)
    {
        Transaction = transaction;
    }

    protected ITransaction Transaction { get; set; }

    public abstract void Perform(ICommand command);
    public abstract void Cancel(ICommand command);
}