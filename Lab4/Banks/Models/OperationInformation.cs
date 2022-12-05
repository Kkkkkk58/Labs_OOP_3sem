using Banks.BankAccounts.Abstractions;
using Banks.Exceptions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public class OperationInformation : IOperationInformation, IEquatable<OperationInformation>
{
    public OperationInformation(ICommandExecutingBankAccount account, MoneyAmount operatedAmount, DateTime initTime)
    {
        Id = Guid.NewGuid();
        BankAccount = account;
        OperatedAmount = operatedAmount;
        InitTime = initTime;
    }

    public Guid Id { get; }
    public ICommandExecutingBankAccount BankAccount { get; }
    public Guid AccountId => BankAccount.Id;
    public MoneyAmount OperatedAmount { get; private set; }
    public DateTime InitTime { get; }
    public DateTime? CompletionTime { get; private set; }
    public bool IsCompleted => CompletionTime.HasValue;

    public void SetCompletionTime(DateTime completionTime)
    {
        if (CompletionTime is not null)
            throw OperationInformationException.IsAlreadyCompleted();

        CompletionTime = completionTime;
    }

    public void SetOperatedAmount(MoneyAmount operatedAmount)
    {
        OperatedAmount = operatedAmount;
    }

    public bool Equals(OperationInformation? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as OperationInformation);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}