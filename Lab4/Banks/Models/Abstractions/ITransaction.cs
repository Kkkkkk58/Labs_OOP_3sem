namespace Banks.Models.Abstractions;

public interface ITransaction
{
    OperationInformation Information { get; }
    void ChangeState(ITransactionState state);
    void Perform();
    void Cancel();
}