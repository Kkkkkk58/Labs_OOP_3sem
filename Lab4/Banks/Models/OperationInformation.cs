using Banks.BankAccounts.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public class OperationInformation : IOperationInformation
{
    public OperationInformation(ICommandExecutingBankAccount account, MoneyAmount operatedAmount, DateTime initTime, string description)
    {
        Id = Guid.NewGuid();
        BankAccount = account;
        OperatedAmount = operatedAmount;
        InitTime = initTime;
        Description = description;
    }

    public Guid Id { get; }
    public ICommandExecutingBankAccount BankAccount { get; }
    public Guid AccountId => BankAccount.Id;
    public MoneyAmount OperatedAmount { get; private set; }
    public DateTime InitTime { get; }
    public DateTime? CompletionTime { get; private set; }
    public bool IsCompleted => CompletionTime.HasValue;
    public string Description { get; private set; }

    public void SetCompletionTime(DateTime completionTime)
    {
        if (CompletionTime is not null)
            throw new NotImplementedException();

        CompletionTime = completionTime;
    }

    public void SetOperatedAmount(MoneyAmount operatedAmount)
    {
        OperatedAmount = operatedAmount;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }
}