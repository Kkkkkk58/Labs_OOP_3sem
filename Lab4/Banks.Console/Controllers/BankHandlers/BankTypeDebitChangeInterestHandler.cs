using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeDebitChangeInterestHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeDebitChangeInterestHandler(AppContext context)
        : base("interest")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        var typeId = args[2].ToGuid();
        decimal interestOnBalance = decimal.Parse(args[3]);

        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        bank.AccountTypeManager.ChangeInterestOnBalance(typeId, interestOnBalance);
        base.Handle(args);
    }
}