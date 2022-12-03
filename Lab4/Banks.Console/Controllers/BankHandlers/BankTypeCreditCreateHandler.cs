using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeCreditCreateHandler : Handler
{
    private readonly AppContext _context;
    public BankTypeCreditCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        System.Console.Write("Enter debt limit: ");
        MoneyAmount debtLimit = System.Console.ReadLine()?.ToMoneyAmount() ?? throw new NotImplementedException();
        System.Console.Write("Enter charge: ");
        MoneyAmount charge = System.Console.ReadLine()?.ToMoneyAmount() ?? throw new NotImplementedException();
        ICreditAccountType type = bank.AccountTypeManager.CreateCreditAccountType(debtLimit, charge);

        // TODO
        System.Console.WriteLine(type.Id);
        base.Handle(args);
    }
}