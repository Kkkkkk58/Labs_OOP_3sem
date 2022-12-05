using Banks.Models;

namespace Banks.Transactions.Abstractions;

public interface ITransaction
{
    OperationInformation Information { get; }
    void ChangeState(ITransactionState state);
    void Perform();
    void Cancel();
}