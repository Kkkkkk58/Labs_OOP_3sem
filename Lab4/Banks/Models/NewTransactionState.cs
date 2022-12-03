using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public sealed class NewTransactionState : TransactionState
{
    public NewTransactionState(ITransaction transaction)
        : base(transaction)
    {
        if (transaction.Information.IsCompleted)
            throw new NotImplementedException();
    }

    public override void Perform(ICommand command)
    {
        Transaction.ChangeState(new RunningTransactionState(Transaction));
        Transaction.Information.BankAccount.ExecuteCommand(command, Transaction);
    }

    public override void Cancel(ICommand command)
    {
        throw new NotImplementedException();
    }
}