using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Console.ViewModels;
using Banks.Models.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class AccountOperationHistoryHandler : Handler
{
    private readonly AppContext _context;

    public AccountOperationHistoryHandler(AppContext context)
        : base("account")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var accountId = args[1].ToGuid();

        IReadOnlyCollection<IOperationInformation> operationHistory = _context
            .CentralBank
            .Banks
            .Single(bank => bank.FindAccount(accountId) is not null)
            .GetAccount(accountId)
            .OperationHistory;

        foreach (IOperationInformation operationInformation in operationHistory)
        {
            _context.Writer.WriteLine(new OperationInformationViewModel(operationInformation));
        }
    }
}