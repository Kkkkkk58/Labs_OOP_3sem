using Banks.Models.Abstractions;

namespace Banks.Console.ViewModels;

public record OperationInformationViewModel
{
    private readonly IOperationInformation _operationInformation;

    public OperationInformationViewModel(IOperationInformation operationInformation)
    {
        _operationInformation = operationInformation;
    }

    public override string ToString()
    {
        return
            $"Operation {_operationInformation.Id} with account {_operationInformation.AccountId} and money amount {_operationInformation.OperatedAmount}\n" +
            $"Is completed: {_operationInformation.IsCompleted}\n" +
            $"Performing time: {_operationInformation.InitTime} - {_operationInformation.CompletionTime}";
    }
}