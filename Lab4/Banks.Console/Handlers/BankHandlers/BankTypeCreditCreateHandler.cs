using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeCreditCreateHandler : Handler
{
    private readonly AppContext _context;
    public BankTypeCreditCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        _context.Writer.Write("Enter debt limit: ");
        MoneyAmount debtLimit = System.Console.ReadLine()?.ToMoneyAmount() ?? throw new NotImplementedException();
        _context.Writer.Write("Enter charge: ");
        MoneyAmount charge = System.Console.ReadLine()?.ToMoneyAmount() ?? throw new NotImplementedException();
        ICreditAccountType type = bank.AccountTypeManager.CreateCreditAccountType(debtLimit, charge);

        // TODO
        _context.Writer.WriteLine(type.Id);
    }
}