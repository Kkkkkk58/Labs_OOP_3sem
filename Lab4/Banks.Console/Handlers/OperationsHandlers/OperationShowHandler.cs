using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Console.ViewModels;
using Banks.Models.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class OperationShowHandler : Handler
{
    private readonly AppContext _context;

    public OperationShowHandler(AppContext context)
        : base("show")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        IOperationInformation operationInformation = GetOperationInformation();
        _context.Writer.WriteLine(new OperationInformationViewModel(operationInformation));
    }

    private IOperationInformation GetOperationInformation()
    {
        _context.Writer.Write("Enter operation id: ");
        var operationId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Operations
            .Single(operation => operation.Id.Equals(operationId));
    }
}