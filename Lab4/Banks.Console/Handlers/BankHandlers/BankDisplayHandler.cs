using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Console.ViewModels;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankDisplayHandler : Handler
{
    private readonly AppContext _context;

    public BankDisplayHandler(AppContext context)
        : base("display")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context
            .CentralBank
            .Banks
            .Single(b => b.Id.Equals(bankId));

        _context.Writer.WriteLine(new BankViewModel(bank));
    }
}