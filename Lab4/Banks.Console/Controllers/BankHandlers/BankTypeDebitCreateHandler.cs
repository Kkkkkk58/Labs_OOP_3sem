using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeDebitCreateHandler : Handler
{
    private readonly AppContext _context;
    public BankTypeDebitCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        System.Console.Write("Enter interest on balance: ");
        decimal interestOnBalance = decimal.Parse(System.Console.ReadLine() ?? throw new NotImplementedException());
        System.Console.Write("Enter interest calculation period: ");
        var period = TimeSpan.Parse(System.Console.ReadLine() ?? throw new NotImplementedException());

        IDebitAccountType type = bank.AccountTypeManager.CreateDebitAccountType(interestOnBalance, period);

        // TODO
        System.Console.WriteLine(type.Id);
        base.Handle(args);
    }
}