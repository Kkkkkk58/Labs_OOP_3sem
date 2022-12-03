namespace Banks.Models.Abstractions;

public interface IOperationInformation
{
    public Guid Id { get; }
    public Guid AccountId { get; }
    public MoneyAmount OperatedAmount { get; }
    public DateTime InitTime { get; }
    public DateTime? CompletionTime { get; }
    public bool IsCompleted { get; }
    public string Description { get; }
}